using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Tests;

public class OrdersControllerTests
{
    private OrdersControllerTests _sut;
    private List<ValidationResult> _validationResult;
    private Mock<IOrdersService> _ordersServiceMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = APIMapperConfigStorage.GetInstance();
        _ordersServiceMock = new Mock<IOrdersService>();
        _sut = new OrdersController(_ordersServiceMock.Object, _mapper);
        _validationResult = new List<ValidationResult>();
    }
}
