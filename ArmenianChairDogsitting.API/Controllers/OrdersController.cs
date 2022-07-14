using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;

namespace ArmenianChairDogsitting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [HttpPost]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] AbstractOrderRequest order)
        {
            var result = _ordersService.AddOrder(_mapper.Map<Order>(order));
            return Created($"{this.GetUri()}/{result}", result);
        }

        [HttpPatch("{id}")]
        [AuthorizeByRole(Role.Sitter)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult ChangeOrderStatus([FromBody] Status orderStatus, int id)
        {
            _ordersService.UpdateOrderStatus(orderStatus, id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(AbstractOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<AbstractOrderResponse> GetOrderById(int id)
        {
            var result = _ordersService.GetOrderById(id);
            return Ok(_mapper.Map<AbstractOrderResponse>(result));
        }

        [HttpGet]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(List<AbstractOrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<List<AbstractOrderResponse>> GetAllOrders()
        {
            var result = _ordersService.GetAllOrders();
            return Ok(_mapper.Map<List<AbstractOrderResponse>>(result));
        }
    }
}
