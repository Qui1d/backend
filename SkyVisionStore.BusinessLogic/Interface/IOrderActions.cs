using OrderEntity = SkyVisionStore.Domain.Entities.Order.Order;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IOrderActions
    {
        List<OrderEntity> GetOrdersByUserId(int userId);
        OrderEntity? GetOrderById(int orderId);
        OrderEntity CreateOrder(CreateOrderModel model);
        OrderEntity? UpdateOrderStatus(int orderId, OrderStatus status);
        bool DeleteOrder(int orderId);
    }
}