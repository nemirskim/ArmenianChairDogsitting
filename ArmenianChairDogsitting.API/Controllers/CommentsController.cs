using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Business.Interfaces;

namespace ArmenianChairDogsitting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentsService _service;
        public CommentsController(ICommentsService commentService)
        {
            _service = commentService;
        }

        [HttpDelete("{id}")]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public ActionResult DeleteCommentById(int id)
        {
            _service.DeleteCommentById(id);
            return NoContent();
        }

    }
}
