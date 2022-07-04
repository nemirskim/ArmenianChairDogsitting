using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Roles;
using ArmenianChairDogsitting.API.Extensions;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class AnimalsController : Controller
{

    [AuthorizeByRole(Role.Client)]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> Add([FromBody] DogRequest animal)
    {
        int id = 12;
        return Created($"{this.GetUri()}/{id}", id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DogMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult <DogMainInfoResponse> GetById(int id)
    {
        return Ok(new DogMainInfoResponse());
    }

    [HttpGet("{id}/client")]
    [ProducesResponseType(typeof(List<DogAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<DogAllInfoResponse>> GetAllByClient(int id)
    {
        return Ok(new List<DogAllInfoResponse>());
    }


    [AuthorizeByRole(Role.Client)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult UpdateById(int id)
    {
        return NoContent();
    }


    [AuthorizeByRole(Role.Client)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult DeleteById(int id)
    {
        return NoContent();
    }
}
