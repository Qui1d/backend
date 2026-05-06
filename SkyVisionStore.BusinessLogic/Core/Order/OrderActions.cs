using SkyVisionStore.BusinessLogic.Interface;
using OrderEntity = SkyVisionStore.Domain.Entities.Order.Order;
using OrderItemEntity = SkyVisionStore.Domain.Entities.Order.OrderItem;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;

namespace SkyVisionStore.BusinessLogic.Core.Order
{
    public class OrderActions : IOrderActions
    {
        private static readonly List<OrderEntity> _orders = new();
        private static int _nextOrderId = 1;
        private static int _nextOrderItemId = 1;

        public List<OrderEntity> GetOrdersByUserId(int userId)
        {
            return _orders.Where(o => o.UserId == userId).ToList();
        }

        public OrderEntity? GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        public OrderEntity CreateOrder(CreateOrderModel model)
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
            return order;
        }

        public OrderEntity? UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            order.Status = status;
            return order;
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
    }
}