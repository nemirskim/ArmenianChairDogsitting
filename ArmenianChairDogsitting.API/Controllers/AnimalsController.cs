using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Roles;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class AnimalsController : Controller
{

    [Authorize(Roles = nameof(Role.Client))]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> Add( [FromBody] AnimalRequest animal)
    {
        int id = 12;
        return Created($"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}/{id}", id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult <AnimalMainInfoResponse> GetById(int id)
    {
        return Ok(new AnimalMainInfoResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<AnimalAllInfoResponse>> GetAll()
    {
        return Ok(new List<AnimalAllInfoResponse>());
    }


    [Authorize(Roles = nameof(Role.Client))]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateById(int id)
    {
        return NoContent();
    }


    [Authorize(Roles = nameof(Role.Client))]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult DeleteById(int id)
    {
        return NoContent();
    }
}
