using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        int AddOrder(Order order);
        int UpdateOrderStatus(Status status, int orderId);
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
    }
}
