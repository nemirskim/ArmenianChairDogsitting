using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API.Roles;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        [HttpPost]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult AddOrder([FromBody] AbstractOrderRequest order)
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        [AuthorizeByRole(Role.Sitter)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult ChangeOrderStatus([FromBody] AbstractOrderRequest order, int id)
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(AbstractOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AbstractOrderResponse> GetOrderById(int id)
        {
            return Ok(new AbstractOrderResponse());
        }

        [HttpGet]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(List<AbstractOrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<AbstractOrderResponse>> GetAllOrders()
        {
            return Ok(new List<AbstractOrderResponse>());
        }
    }
}
