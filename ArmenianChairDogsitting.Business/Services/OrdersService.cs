using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Business.Services;

public class OrdersService : IOrdersService
{
    IOrdersRepository _ordersRepository;
    ISittersService _sittersService;

    public OrdersService(IOrdersRepository commentsRepository, ISittersService sittersService)
    {
        _ordersRepository = commentsRepository;
        _sittersService = sittersService;
    }
    public int AddOrder(Order order)
    {
        order.Status = Status.Created;

        if (!IsOkToAddOrder(order))
            throw new AdditionalOrderException("invalid request");

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

    private bool IsOkToAddOrder(Order order)
    {
        var choosenSitter = _sittersService.GetById(order.Sitter.Id);

        var activeOrders = choosenSitter!.Orders.Where(o => o.Status == Status.InProgress ||
        o.Status == Status.Created);

        if (!(order.WorkDate >= DateTime.Now.AddHours(1)))
            return false;

        if(order.Type == Service.Walk)
        {
            if (!activeOrders.Any(o => o.Type == Service.DailySitting || o.Type == Service.Overexpose))
                return false;
        }

        if (activeOrders.Count() != 0)
            return false;

        return true;
    }
}
