using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SittersController : Controller
{

    private readonly ISitterService _sittersService;
    private readonly IMapper _mapper;

    public SittersController(ISitterService sittersService, IMapper mapper)
    {
        _sittersService = sittersService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddSitter([FromBody] SitterRequest sitterRequest)
    {
        var result = _sittersService.Add(_mapper.Map<Sitter>(sitterRequest));
        return Created($"{this.GetUri()}/{result}", result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SitterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<SitterMainInfoResponse> GetSitterById(int id)
    {
        var result = _sittersService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(_mapper.Map<SitterMainInfoResponse>(result));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SitterAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<SitterAllInfoResponse>> GetAllSitters()
    {
        var result = _sittersService.GetSitters();

        if (result == null)
            return NotFound();

        return Ok(_mapper.Map<List<SitterAllInfoResponse>>(result));
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdateSitter(SitterUpdateRequest sitterUpdateRequest, int id)
    {
        _sittersService.Update(_mapper.Map<Sitter>(sitterUpdateRequest), id);
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult RemoveSitterById(int id)
    {
        _sittersService.RemoveById(id);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult RestoreSitterById(int id)
    {
        _sittersService.RestoreById(id);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePasswordSitter(int id, [FromBody]UserUpdatePasswordRequest sitterPasswordForUpdate)
    {
        _sittersService.UpdatePassword(id, _mapper.Map<Sitter>(sitterPasswordForUpdate));
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}/{catalog}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePriceCatalogSitter(int id, List<PriceCatalog> priceCatalog)
    {
        _sittersService.UpdatePriceCatalog(id, priceCatalog);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpGet("{id}/schedule")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult GetAllSittersWithWorkTimes(int id)
    {
        return NoContent();
    }
}
