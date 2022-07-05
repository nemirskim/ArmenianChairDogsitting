﻿using Microsoft.EntityFrameworkCore;
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
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public List<Order> GetAllOrders() => _context.Orders.ToList();

        public Order? GetOrderById(int id) =>
        _context.Orders
            .Include(o => o.Animals)
            .FirstOrDefault(o => o.Id == id);

        public void UpdateOrderStatus(Status status, int orderId)
        {
            var choosenOrder = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            choosenOrder!.Status = status.ToString();
            _context.Orders.Update(choosenOrder);
            _context.SaveChanges();
        }
    }
}