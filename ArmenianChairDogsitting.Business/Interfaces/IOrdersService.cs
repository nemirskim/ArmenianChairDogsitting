using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface IOrdersService
{
    int AddOrder(Order order);
    void UpdateOrderStatus(Status status, int orderId);
    Order GetOrderById(int orderId);
    List<Order> GetAllOrders();
    List<Comment> GetCommentsByOrderId(int orderId);
    int AddCommentToOrder(int orderId, Comment commentToAdd);
    void DeleteOrderById(int id);
    void UpdateOrder(PropertiesToChangeOrder orderProperties, int orderId);
}
