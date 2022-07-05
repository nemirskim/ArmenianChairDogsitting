using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        [HttpPost]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] AbstractOrderRequest order)
        {
            int id = 0;
            return Created($"{this.GetUri()}/{id}", id);
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
            return Ok(new OrderWalkResponse());
        }

        [HttpGet]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<AbstractOrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<List<AbstractOrderResponse>> GetAllOrders()
        {
            return Ok(new List<AbstractOrderResponse>());
        }
    }
}
