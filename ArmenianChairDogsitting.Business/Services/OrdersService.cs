using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;

namespace ArmenianChairDogsitting.Business.Services;

public class OrdersService : IOrdersService
{
    IOrdersRepository _ordersRepository;
    IClientsRepository _clientsRepository;
    ISittersRepository _sittersRepository;

    public OrdersService(
        IOrdersRepository ordersRepository,
        IClientsRepository clientsRepository,
        ISittersRepository sittersRepository)
    {
        _ordersRepository = ordersRepository;
        _clientsRepository = clientsRepository;
        _sittersRepository = sittersRepository;
    }
    public int AddOrder(Order order)
    {
        order.Status = Status.Created;
        order.Client = _clientsRepository.GetClientById(order.Client.Id);
        order.Sitter = _sittersRepository.GetById(order.Sitter.Id);
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

        return _ordersRepository.GetCommentsByOrderId(orderId);
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

    public void UpdateOrder(UpdateOrderModel orderProperties, int orderId)
    {
        var chosenOrder = _ordersRepository.GetOrderById(orderId);

        if (chosenOrder == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenCommentDoesNotExist}{orderId}");

        if (chosenOrder.Status == Status.InProgress)
            throw new ForbiddenException($"{ExceptionMessage.ActionIsNotAllowed}{chosenOrder.Status}");

        _ordersRepository.ChangeOrder(orderProperties, orderId);
    }
}
