using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Enums;
using AutoMapper;
using ArmenianChairDogsitting.Business;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class AnimalsController : Controller
{
    private readonly IAnimalsService _animalsService;
    private readonly IMapper _mapper;

    public AnimalsController(IAnimalsService animalsService, IMapper mapper)
    {
        _animalsService = animalsService;
        _mapper = mapper;
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> AddAnimal([FromBody] DogRequest request)
    {
        var result = _animalsService.AddAnimal(_mapper.Map <Animal>(request));
        return Created($"{this.GetUri()}/{result}", result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DogMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult <DogMainInfoResponse> GetAnimalById(int id)
    {
        var result = _animalsService.GetAnimalById(id);

        if (result is null)
            return NotFound();
        else
            return Ok(_mapper.Map<DogMainInfoResponse>(result));
    }

    [HttpGet("{id}/client")]
    [AuthorizeByRole(Role.Client)]
    [ProducesResponseType(typeof(List<DogAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult<List<DogAllInfoResponse>> GetAllAnimalsByClient(int id)
    {
        if (this.GetUserRole() != Role.Admin &&
            this.GetUserId() != id)
            return Forbid();

        var animals = _animalsService.GetAllAnimalsByClient(id);

        return Ok(_mapper.Map<List<DogAllInfoResponse>>(animals));
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult UpdateAnimalById([FromBody] DogUpdateRequest request, int id)
    {
        _animalsService.UpdateAnimal(_mapper.Map<Animal>(request), id);
        return Ok();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RemoveAnimal(int id)
    {
        _animalsService.RemoveOrRestoreAnimal(id, true);
        return NoContent();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RestoreAnimal(int id)
    {
        _animalsService.RemoveOrRestoreAnimal(id, false);
        return NoContent();
    }
}
