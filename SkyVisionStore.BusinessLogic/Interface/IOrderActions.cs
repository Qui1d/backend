using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IOrderActions
    {
        List<OrderInfoModel> GetAll();

        List<OrderInfoModel> GetOrdersByUserId(int userId);

        OrderInfoModel? GetOrderById(int orderId);

        OrderInfoModel CreateOrder(CreateOrderModel model);

        OrderInfoModel? CheckoutFromCart(int userId);

        OrderInfoModel? UpdateOrder(int orderId, OrderUpdateModel model);

        OrderInfoModel? UpdateOrderStatus(int orderId, OrderStatus status);

        bool DeleteOrder(int orderId);
    }
}