using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SittersController : Controller
{

    private readonly ISitterRepository _sittersRepository;

    public SittersController(ISitterRepository sittersRepository)
    {
        _sittersRepository = sittersRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddSitter([FromBody] SitterRequest sitterRequest)
    {
        var sitter = new Sitter
        {
            Name = sitterRequest.Name,
            LastName = sitterRequest.LastName,
            Phone = sitterRequest.Phone,
            Email = sitterRequest.Email,
            Age = sitterRequest.Age,
            Experience = sitterRequest.Experience,
            Sex = sitterRequest.Sex,
            Description = sitterRequest.Description,
            PriceCatalog = sitterRequest.PriceCatalog
        };

        var result = _sittersRepository.AddSitter(sitter);
        return Created($"{this.GetUri()}/{result}", result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SitterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SitterMainInfoResponse> GetSitterById(int id)
    {
        var result = _sittersRepository.GetSitterById(id);

        if (result is null)
            return NotFound();
        else
            return Ok(result);
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateSitter(SitterRequest sitterRequest, int id)
    {
        var sitter = new Sitter
        {
            Name = sitterRequest.Name,
            LastName = sitterRequest.LastName,
            Phone = sitterRequest.Phone,
            Email = sitterRequest.Email,
            Age = sitterRequest.Age,
            Experience = sitterRequest.Experience,
            Sex = sitterRequest.Sex,
            Description = sitterRequest.Description,
            PriceCatalog = sitterRequest.PriceCatalog
        };

        _sittersRepository.UpdateSitter(sitter, id);

        return NoContent();
    }

    [AuthorizeByRole]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
   // [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult RestoreSitterById(int id)
    {
        return NoContent();
    }
}
