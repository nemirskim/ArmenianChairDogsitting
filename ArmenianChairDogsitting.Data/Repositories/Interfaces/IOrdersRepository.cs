using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Repositories
{
    public interface IOrdersRepository
    {
        int AddOrder(Order order);
        void UpdateOrderStatus(Status status, int orderId);
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        int AddCommentToOrder(int orderId, Comment commentToAdd);
        void DeleteOrderById(int id);
        void ChangeOrder(UpdateOrderModel orderProperties, int orderId);
        List<Comment> GetCommentsByOrderId(int id);
    }
}
