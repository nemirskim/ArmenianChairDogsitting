using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;

namespace ArmenianChairDogsitting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class СommentsController : Controller
    {
        private readonly ICommentsService _service;
        private IMapper _mapper;
        public СommentsController(ICommentsService commentService, IMapper mapper)
        {
            _service = commentService;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult DeleteCommentById(int id)
        {
            _service.DeleteCommentById(id);
            return NoContent();
        }

    }
}
