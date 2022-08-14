using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ArmenianChairDogsittingContext _context;

        public OrdersRepository(ArmenianChairDogsittingContext context)
        {
            _context = context;
        }

        public int AddOrder(Order order)
        {
            order.DateCreate = DateTime.Now;
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public List<Order> GetAllOrders() => _context.Orders
            .Where(o => !o.IsDeleted)
            .ToList();

        public Order? GetOrderById(int id) =>
        _context.Orders
            .Include(o => o.Animals)
            .Include(o => o.Comments)
            .FirstOrDefault(o => o.Id == id);

        public void UpdateOrderStatus(Status status, int orderId)
        {
            var choosenOrder = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            choosenOrder!.Status = status;
            choosenOrder!.DateUpdated = DateTime.Now;
            _context.Orders.Update(choosenOrder);
            _context.SaveChanges();
        }

        public int AddCommentToOrder(int orderId, Comment commentToAdd)
        {
            _context.Orders
                .FirstOrDefault(o => o.Id == orderId)!.Comments
                .Add(commentToAdd);
            _context.SaveChanges();
            return commentToAdd.Id;
        }

        public void DeleteOrderById(int id)
        {
            var orderToDelete = _context.Orders.FirstOrDefault(o => o.Id == id);
            orderToDelete!.IsDeleted = true;
            orderToDelete!.DateUpdated = DateTime.Now;
            _context.Orders.Update(orderToDelete);
            _context.SaveChanges();
        }

        public void ChangeOrder(UpdateOrderModel orderProperties, int orderId)
        {
            var orderToChange = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            orderToChange!.Animals = orderProperties.Animals;
            orderToChange!.WorkDate = orderProperties.WorkDate;
            orderToChange!.District = orderProperties.District;
            orderToChange.Address = orderProperties.Address;
            orderToChange!.DateUpdated = DateTime.Now;
            _context.Orders.Update(orderToChange);
            _context.SaveChanges();
        }

        public List<Comment> GetCommentsByOrderId(int id) =>
            _context.Comments
                .Where(
                    c => c.Order.Id == id &&
                    !c.IsDeleted)
                .ToList();
    }
}
