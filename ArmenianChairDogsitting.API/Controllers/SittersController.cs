using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SittersController : Controller
{

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddSitter([FromBody] SitterRequest sitter)
    {
        int id = 42;
        return Created($"{this.GetUri()}/{id}", id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SitterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SitterMainInfoResponse> GetSitterById(int id)
    {
        return Ok(new SitterMainInfoResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SitterAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<SitterAllInfoResponse>> GetAllSitters()
    {
        return Ok(new List<SitterAllInfoResponse>());
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateSitterById(int id)
    {
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public ActionResult RemoveSitterById(int id)
    {
        return NoContent();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpGet("{id}/Schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult GetAllSittersWithWorkTimes(int id)
    {
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeactivateSitterById(int id)
    {
        return NoContent();
    }
}
