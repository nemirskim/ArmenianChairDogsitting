using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class AnimalsController : Controller
{
    private readonly IAnimalsRepository _animalsRepository;
    public AnimalsController(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> AddAnimal([FromBody] DogRequest animal)
    {
        int id = 12;
        return Created($"{this.GetUri()}/{id}", id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DogMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult <DogMainInfoResponse> GetAnimalById(int id)
    {
        return Ok(new DogMainInfoResponse());
    }

    [HttpGet("{id}/client")]
    [ProducesResponseType(typeof(List<DogAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<DogAllInfoResponse>> GetAllAnimalsByClient(int id)
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
    public ActionResult UpdateAnimal([FromBody] DogUpdateRequest request, int id)
    {
        return NoContent();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult RemoveAnimalById(int id)
    {
        return NoContent();
    }
}
