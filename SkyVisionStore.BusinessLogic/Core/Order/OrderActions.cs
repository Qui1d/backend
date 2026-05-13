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
        public List<OrderInfoModel> GetAll()
        {
            using var db = new SkyVisionStoreContext();

            return db.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => ToInfoModel(o))
                .ToList();
        }

        public List<OrderInfoModel> GetOrdersByUserId(int userId)
        {
            using var db = new SkyVisionStoreContext();

            return db.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => ToInfoModel(o))
                .ToList();
        }

        public OrderInfoModel? GetOrderById(int orderId)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            return ToInfoModel(order);
        }

        public OrderInfoModel CreateOrder(CreateOrderModel model)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(u => u.Id == model.UserId);

            if (!userExists)
            {
                throw new InvalidOperationException($"User with ID {model.UserId} does not exist.");
            }

            var orderItems = model.Items.Select(item =>
            {
                var productExists = db.Products.Any(p => p.Id == item.ProductId);

                if (!productExists)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                }

                return new OrderItemEntity
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity <= 0 ? 1 : item.Quantity,
                    UnitPrice = item.UnitPrice
                };
            }).ToList();

            var totalAmount = model.TotalAmount > 0
                ? model.TotalAmount
                : orderItems.Sum(item => item.UnitPrice * item.Quantity);

            var order = new OrderEntity
            {
                UserId = model.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = totalAmount,
                Items = orderItems
            };

            db.Orders.Add(order);
            db.SaveChanges();

            var createdOrder = db.Orders
                .Include(o => o.Items)
                .First(o => o.Id == order.Id);

            return ToInfoModel(createdOrder);
        }

        public OrderInfoModel? UpdateOrder(int orderId, OrderUpdateModel model)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var userExists = db.Users.Any(u => u.Id == model.UserId);

            if (!userExists)
            {
                return null;
            }

            db.OrderItems.RemoveRange(order.Items);

            var newItems = model.Items.Select(item =>
            {
                var productExists = db.Products.Any(p => p.Id == item.ProductId);

                if (!productExists)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                }

                return new OrderItemEntity
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity <= 0 ? 1 : item.Quantity,
                    UnitPrice = item.UnitPrice
                };
            }).ToList();

            order.UserId = model.UserId;
            order.Status = model.Status;
            order.TotalAmount = model.TotalAmount > 0
                ? model.TotalAmount
                : newItems.Sum(item => item.UnitPrice * item.Quantity);
            order.Items = newItems;

            db.SaveChanges();

            var updatedOrder = db.Orders
                .Include(o => o.Items)
                .First(o => o.Id == order.Id);

            return ToInfoModel(updatedOrder);
        }

        public OrderInfoModel? UpdateOrderStatus(int orderId, OrderStatus status)
        {
            using var db = new SkyVisionStoreContext();

            var order = db.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);

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
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);

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
                UnitPrice = item.UnitPrice
            };
        }
    }
}