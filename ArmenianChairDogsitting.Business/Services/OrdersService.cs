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
    IClientsService _clientsService;
    ISittersService _sittersService;
    IPromocodesService _promocodesService;

    public OrdersService(
        IOrdersRepository ordersRepository,
        IClientsService clientsService,
        ISittersService sittersService,
        IPromocodesService promocodesService)
    {
        _ordersRepository = ordersRepository;
        _clientsService = clientsService;
        _sittersService = sittersService;
        _promocodesService = promocodesService;
    }
    public int AddOrder(Order order, Service orderType)
    {
        order.Status = Status.Created;
        order.Type = orderType;
        order.Client = _clientsService.GetClientById(order.Client.Id);
        order.Sitter = _sittersService.GetById(order.Sitter.Id);
        order.Price = GetOrderPrice(order);

        if(!IsOkToAddOrder(order))
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

        var discount = _promocodesService.GetDiscount(chosenOrder.Client.Promocode!);
        chosenOrder.Price *= discount;

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

    private decimal GetOrderPrice(Order order)
    {
        switch (order.Type)
        {
            case Service.Overexpose:
                return order.Sitter.PriceCatalog.Find(p => p.Service == Service.Overexpose).Price;
            case Service.DailySitting:
                return order.Sitter.PriceCatalog.Find(p => p.Service == Service.DailySitting).Price;
            case Service.SittingForDay:
                return order.Sitter.PriceCatalog.Find(p => p.Service == Service.SittingForDay).Price;
            case Service.Walk:
                return order.Sitter.PriceCatalog.Find(p => p.Service == Service.Walk).Price;
            default:
                throw new SittersServiceException(ExceptionMessage.SitterHasNoService);
        }
    }

    private bool IsOkToAddOrder(Order order)
    {
        var choosenSitter = _sittersService.GetById(order.Sitter.Id);

        var activeOrders = choosenSitter!.Orders.Where(o => o.Status == Status.InProgress ||
        o.Status == Status.Created);

        if (!(order.WorkDate >= DateTime.Now.AddHours(1)))
            return false;

        if (order.Type == Service.Walk)
        {
            if (
                activeOrders.Any(o => (o.Type == Service.Walk && 
                (o.WorkDate >= order.WorkDate.AddHours(-1) && o.WorkDate <= order.WorkDate.AddHours(1)) ||
                o.Type == Service.SittingForDay))
                )
                return false;

            if (activeOrders.Any(o => (o.Type == Service.DailySitting || o.Type == Service.Overexpose) &&
            order.WalkQuantity <= 2))
                return false;

            if (order.Client.Orders.Any(o => (bool)o.IsTrial!) &&
                (bool)order.IsTrial!)
                return false;
        }


        if (activeOrders.Count() != 0 && order.Type != Service.Walk)
            return false;

        return true;
    }
}