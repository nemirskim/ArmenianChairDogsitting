﻿using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Business.Services;

public class OrdersService : IOrdersService
{
    IOrdersRepository _ordersRepository;

    public OrdersService(IOrdersRepository commentsRepository)
    {
        _ordersRepository = commentsRepository;
    }
    public int AddOrder(Order order)
    {
        order.Status = Status.Created;
        return _ordersRepository.AddOrder(order);
    }

    public List<Order> GetAllOrders()
    {
        var allOrders = _ordersRepository.GetAllOrders();

        return allOrders;
    }

    public Order GetOrderById(int orderId)
    {
        var chosenOrder = _ordersRepository.GetOrderById(orderId);

        return chosenOrder;
    }

    public void UpdateOrderStatus(Status status, int orderId)
    {
        var chosenOrder = _ordersRepository.GetOrderById(orderId);

        if (chosenOrder == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenCommentDoesNotExist}{orderId}");

        _ordersRepository.UpdateOrderStatus(status, orderId);
    }

    public List<Comment> GetCommentsByOrderId(int orderId)
    {
        var chosenOrder = _ordersRepository.GetOrderById(orderId);

        if (chosenOrder == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenCommentDoesNotExist}{orderId}");

        return chosenOrder.Comments;
    }

    public int AddCommentToOrder(int orderId, Comment commentToAdd)
    {
        var chosenOrder = _ordersRepository.GetOrderById(orderId);

        if (chosenOrder == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenCommentDoesNotExist}{orderId}");

        return _ordersRepository.AddCommentToOrder(orderId, commentToAdd);
    }

    public void DeleteOrderById(int id)
    {
        var chosenOrder = _ordersRepository.GetOrderById(id);

        if (chosenOrder == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenCommentDoesNotExist}{id}");

        if(chosenOrder.Status == Status.InProgress)
            throw new ForbiddenException($"{ExceptionMessage.ActionIsNotAllowed}{chosenOrder.Status}");
        
        _ordersRepository.DeleteOrderById(id);
    }
}
