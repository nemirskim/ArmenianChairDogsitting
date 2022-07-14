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

    public SittersController()
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddSitter([FromBody] SitterRequest sitterRequest)
    { 
        int result = 1;
        return Created($"{this.GetUri()}/{result}", result);
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdateSitter(SitterUpdateRequest sitterUpdateRequest, int id)
    {
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult RemoveOrRestoreSitterById(int id)
    {
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}/{password}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePasswordSitter(int id, string password)
    {
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}/{catalog}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePriceCatalogSitter(int id, List<PriceCatalog> priceCatalog)
    {
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpGet("{id}/Schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult GetAllSittersWithWorkTimes(int id)
    {
        return NoContent();
    }
}
