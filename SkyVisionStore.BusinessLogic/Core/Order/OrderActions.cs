using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;
using OrderEntity = SkyVisionStore.Domain.Entities.Order.Order;
using OrderItemEntity = SkyVisionStore.Domain.Entities.Order.OrderItem;

namespace SkyVisionStore.BusinessLogic.Core.Order
{
    public class OrderActions : IOrderActions
    {
        private static readonly List<OrderEntity> _orders = new();
        private static int _nextOrderId = 1;
        private static int _nextOrderItemId = 1;

        public List<OrderInfoModel> GetAll()
        {
            return _orders.Select(ToInfoModel).ToList();
        }

        public List<OrderInfoModel> GetOrdersByUserId(int userId)
        {
            return _orders
                .Where(o => o.UserId == userId)
                .Select(ToInfoModel)
                .ToList();
        }

        public OrderInfoModel? GetOrderById(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            return ToInfoModel(order);
        }

        public OrderInfoModel CreateOrder(CreateOrderModel model)
        {
            var order = new OrderEntity
            {
                Id = _nextOrderId++,
                UserId = model.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = model.TotalAmount,
                Items = model.Items.Select(item => new OrderItemEntity
                {
                    Id = _nextOrderItemId++,
                    OrderId = 0,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            foreach (var item in order.Items)
            {
                item.OrderId = order.Id;
            }

            _orders.Add(order);

            return ToInfoModel(order);
        }

        public OrderInfoModel? UpdateOrder(int orderId, OrderUpdateModel model)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == orderId);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.UserId = model.UserId;
            existingOrder.Status = model.Status;
            existingOrder.TotalAmount = model.TotalAmount;

            existingOrder.Items = model.Items.Select(item => new OrderItemEntity
            {
                Id = _nextOrderItemId++,
                OrderId = existingOrder.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList();

            return ToInfoModel(existingOrder);
        }

        public OrderInfoModel? UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            order.Status = status;

            return ToInfoModel(order);
        }

        public bool DeleteOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            _orders.Remove(order);

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