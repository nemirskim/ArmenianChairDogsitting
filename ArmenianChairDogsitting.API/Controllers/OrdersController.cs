using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data;
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

        [HttpPost("walk")]
        [AllowAnonymous]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] OrderWalkRequest order)
        {
            var result = _ordersService.AddOrder(_mapper.Map<Order>(order), Service.Walk);
            return Created($"{this.GetUri()}/{result}", result);
        }

        [HttpPost("overexpose")]
        [AllowAnonymous]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] OrderOverexposeRequest order)
        {
            var result = _ordersService.AddOrder(_mapper.Map<Order>(order), Service.Overexpose);
            return Created($"{this.GetUri()}/{result}", result);
        }

        [HttpPost("daily-sitting")]
        [AllowAnonymous]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] OrderDailySittingRequest order)
        {
            var result = _ordersService.AddOrder(_mapper.Map<Order>(order), Service.DailySitting);
            return Created($"{this.GetUri()}/{result}", result);
        }

        [HttpPost("sitting-for-a-day")]
        [AllowAnonymous]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] OrderSittingForDayRequest order)
        {
            var result = _ordersService.AddOrder(_mapper.Map<Order>(order), Service.SittingForDay);
            return Created($"{this.GetUri()}/{result}", result);
        }

        [HttpPatch("{id}")]
        [AuthorizeByRole(Role.Sitter)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<OrderResponse> GetOrderById(int id)
        {
            var result = _ordersService.GetOrderById(id);
            if (result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<OrderResponse>(result));
            }
        }

        [HttpGet]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
        public ActionResult<List<OrderResponse>> GetAllOrders()
        {
            var result = _ordersService.GetAllOrders();
            return Ok(_mapper.Map<List<OrderResponse>>(result));
        }

        [HttpGet("{id}/comments")]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<CommentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public ActionResult<List<CommentResponse>> GetCommentsByOrderId([FromRoute] int id)
        {
            var result = _ordersService.GetCommentsByOrderId(id);
            return Ok(_mapper.Map<List<Comment>>(result));
        }

        [HttpPost("{id}/comments")]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public ActionResult<int> AddCommentToOrder([FromRoute]int id, [FromBody]CommentRequest commentToAdd)
        {
            var result = _ordersService.AddCommentToOrder(id, _mapper.Map<Comment>(commentToAdd));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public ActionResult DeleteOrderById(int id)
        {
            _ordersService.DeleteOrderById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public ActionResult UpdateOrder([FromBody] UpdateOrderRequest orderProperties, int id)
        {
            _ordersService.UpdateOrder(_mapper.Map<UpdateOrderModel>(orderProperties), id);
            return NoContent();
        }
    }
}
