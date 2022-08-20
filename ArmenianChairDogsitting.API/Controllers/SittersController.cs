using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
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

    private readonly ISittersService _sittersService;
    private readonly IMapper _mapper;

    public SittersController(ISittersService sittersService, IMapper mapper)
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
        if (!(sitterRequest.Age - sitterRequest.Experience >= Constants.minAgeToWork))
            return UnprocessableEntity();

        var sitter = _mapper.Map<Sitter>(sitterRequest);
        sitter.PriceCatalog = _mapper.Map<List<PriceCatalog>>(sitterRequest.PriceCatalog);

        var result = _sittersService.Add(sitter);
        return Created($"{this.GetUri()}/{result}", result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SitterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<SitterMainInfoResponse> GetSitterById(int id)
    {
        var sitter = _sittersService.GetById(id);

        if (sitter == null)
            return NotFound();

        var result = _mapper.Map<SitterMainInfoResponse>(sitter);
        result.PriceCatalog = _mapper.Map<List<PriceCatalogResponse>>(sitter.PriceCatalog);

        return Ok(result);
    }

    [AllowAnonymous]
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
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdateSitter([FromBody] SitterUpdateRequest sitterUpdateRequest)
    {
        if (!(sitterUpdateRequest.Age - sitterUpdateRequest.Experience >= Constants.minAgeToWork))
            return UnprocessableEntity();

        var userId = this.GetUserId();

        _sittersService.Update(_mapper.Map<Sitter>(sitterUpdateRequest), userId.Value);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult RemoveSitterById()
    {
        var userId = this.GetUserId();

        _sittersService.RemoveOrRestoreById(userId.Value, true);
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpPatch("{id}/restore")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult RestoreSitterById(int id)
    {
        _sittersService.RemoveOrRestoreById(id, false);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePasswordSitter([FromBody]UserUpdatePasswordRequest sitterPasswordForUpdate)
    {
        var userId = this.GetUserId();

        _sittersService.UpdatePassword(userId.Value, _mapper.Map<User>(sitterPasswordForUpdate));
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("prices")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePriceCatalogSitter([FromBody] SitterUpdatePriceCatalogRequest sitterForUpdate)
    {
        var userId = this.GetUserId();

        var sitter = _mapper.Map<Sitter>(sitterForUpdate);
        sitter.PriceCatalog = _mapper.Map<List<PriceCatalog>>(sitterForUpdate.PriceCatalog);
        _sittersService.UpdatePriceCatalog(userId.Value, sitter);
        return NoContent();
    }
}
