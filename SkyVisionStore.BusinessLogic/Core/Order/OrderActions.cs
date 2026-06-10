using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;
using OrderEntity = SkyVisionStore.Domain.Entities.Order.Order;
using OrderItemEntity = SkyVisionStore.Domain.Entities.Order.OrderItem;

namespace SkyVisionStore.BusinessLogic.Core.Order
{
    public class OrderActions : IOrderActions
    {
        private const string GameKeySymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public List<OrderInfoModel> GetAll()
        {
            using var db = new SkyVisionStoreContext();

            var orders = db.Orders
                .Include(order => order.Items)
                .OrderByDescending(order => order.CreatedAt)
                .ToList();

            return orders.Select(ToInfoModel).ToList();
        }

        public List<OrderInfoModel> GetOrdersByUserId(int userId)
        {
            using var db = new SkyVisionStoreContext();

            var orders = db.Orders
                .Include(order => order.Items)
                .Where(order => order.UserId == userId)
                .OrderByDescending(order => order.CreatedAt)
                .ToList();

            return orders.Select(ToInfoModel).ToList();
        }

        public OrderInfoModel? GetOrderById(int orderId)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(order => order.Items)
                .FirstOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                return null;
            }

            return ToInfoModel(order);
        }

        public OrderInfoModel? CheckoutFromCart(int userId)
        {
            using var db = new SkyVisionStoreContext();

            var cartItems = db.CartItems
                .Include(cartItem => cartItem.Product)
                .Where(cartItem => cartItem.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                return null;
            }

            var reservedKeys = new HashSet<string>();
            var orderItems = new List<OrderItemEntity>();

            foreach (var cartItem in cartItems)
            {
                var quantity = cartItem.Quantity <= 0 ? 1 : cartItem.Quantity;

                for (var i = 0; i < quantity; i++)
                {
                    orderItems.Add(new OrderItemEntity
                    {
                        ProductId = cartItem.ProductId,
                        ProductName = cartItem.Product.Title,
                        Quantity = 1,
                        UnitPrice = cartItem.Product.Price,
                        GameKey = GenerateUniqueGameKey(db, reservedKeys)
                    });
                }
            }

            var order = new OrderEntity
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Completed,
                TotalAmount = orderItems.Sum(item => item.UnitPrice * item.Quantity),
                Items = orderItems
            };

            db.Orders.Add(order);
            db.CartItems.RemoveRange(cartItems);
            db.SaveChanges();

            return ToInfoModel(order);
        }

        public OrderInfoModel CreateOrder(CreateOrderModel model)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(user => user.Id == model.UserId);

            if (!userExists)
            {
                throw new InvalidOperationException($"User with ID {model.UserId} does not exist.");
            }

            var reservedKeys = new HashSet<string>();
            var orderItems = new List<OrderItemEntity>();

            foreach (var item in model.Items)
            {
                var productExists = db.Products.Any(product => product.Id == item.ProductId);

                if (!productExists)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                }

                var quantity = item.Quantity <= 0 ? 1 : item.Quantity;

                for (var i = 0; i < quantity; i++)
                {
                    orderItems.Add(new OrderItemEntity
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Quantity = 1,
                        UnitPrice = item.UnitPrice,
                        GameKey = GenerateUniqueGameKey(db, reservedKeys)
                    });
                }
            }

            var order = new OrderEntity
            {
                UserId = model.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Completed,
                TotalAmount = orderItems.Sum(item => item.UnitPrice * item.Quantity),
                Items = orderItems
            };

            db.Orders.Add(order);
            db.SaveChanges();

            return ToInfoModel(order);
        }

        public OrderInfoModel? UpdateOrder(int orderId, OrderUpdateModel model)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(order => order.Items)
                .FirstOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var userExists = db.Users.Any(user => user.Id == model.UserId);

            if (!userExists)
            {
                return null;
            }

            db.OrderItems.RemoveRange(order.Items);

            var reservedKeys = new HashSet<string>();
            var newItems = new List<OrderItemEntity>();

            foreach (var item in model.Items)
            {
                var productExists = db.Products.Any(product => product.Id == item.ProductId);

                if (!productExists)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                }

                var quantity = item.Quantity <= 0 ? 1 : item.Quantity;

                for (var i = 0; i < quantity; i++)
                {
                    newItems.Add(new OrderItemEntity
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Quantity = 1,
                        UnitPrice = item.UnitPrice,
                        GameKey = GenerateUniqueGameKey(db, reservedKeys)
                    });
                }
            }

            order.UserId = model.UserId;
            order.Status = model.Status;
            order.TotalAmount = newItems.Sum(item => item.UnitPrice * item.Quantity);
            order.Items = newItems;

            db.SaveChanges();

            return ToInfoModel(order);
        }

        public OrderInfoModel? UpdateOrderStatus(int orderId, OrderStatus status)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(order => order.Items)
                .FirstOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                return null;
            }

            order.Status = status;
            db.SaveChanges();

            return ToInfoModel(order);
        }

        public bool DeleteOrder(int orderId)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(order => order.Items)
                .FirstOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                return false;
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return true;
        }

        private static OrderInfoModel ToInfoModel(OrderEntity order)
        {
            return new OrderInfoModel
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(ToOrderItemInfoModel).ToList()
            };
        }

        private static OrderItemInfoModel ToOrderItemInfoModel(OrderItemEntity item)
        {
            return new OrderItemInfoModel
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                GameKey = item.GameKey
            };
        }

        private static string GenerateUniqueGameKey(
            SkyVisionStoreContext db,
            HashSet<string> reservedKeys
        )
        {
            string gameKey;

            do
            {
                gameKey = GenerateGameKey();
            }
            while (
                reservedKeys.Contains(gameKey) ||
                db.OrderItems.Any(item => item.GameKey == gameKey)
            );

            reservedKeys.Add(gameKey);

            return gameKey;
        }

        private static string GenerateGameKey()
        {
            return $"{GenerateGameKeyPart()}-{GenerateGameKeyPart()}-{GenerateGameKeyPart()}";
        }

        private static string GenerateGameKeyPart()
        {
            var chars = new char[5];

            for (var i = 0; i < chars.Length; i++)
            {
                var index = RandomNumberGenerator.GetInt32(GameKeySymbols.Length);
                chars[i] = GameKeySymbols[index];
            }

            return new string(chars);
        }
    }
}