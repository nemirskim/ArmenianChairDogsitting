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
        [AuthorizeByRole]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult ChangeOrderStatus([FromBody] AbstractOrderRequest order, int id)
        {
            return NoContent();
        }

        //GetById

        //GetAll
    }
}
