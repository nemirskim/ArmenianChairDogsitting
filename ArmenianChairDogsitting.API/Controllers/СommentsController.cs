using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;
using ArmenianChairDogsitting.Data.Entities;

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

        [HttpPost]
        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddComment([FromBody] CommentRequest comment)
        {
            var model = _mapper.Map<Comment>(comment);
            var returnedId = _service.AddComment(model);
            return Created($"{this.GetUri()}/{returnedId}", returnedId);
        }

        [HttpGet]
        [AuthorizeByRole(Role.Sitter, Role.Client)]
        [ProducesResponseType(typeof(List<CommentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<List<CommentResponse>> GetAllComments()
        {
            var comments = _service.GetComments();
            var result = _mapper.Map<List<CommentResponse>>(comments);
            return Ok(result);
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
