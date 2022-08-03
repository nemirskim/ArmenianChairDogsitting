using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminController : Controller
{

    private readonly IAdminService _adminsService;
    private readonly IMapper _mapper;

    public AdminController(IAdminService adminsService, IMapper mapper)
    {
        _adminsService = adminsService;
        _mapper = mapper;
    }

    [AuthorizeByRole]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddAdmin([FromBody] AdminRequest adminRequest)
    {
        var result = _adminsService.AddAdmin(_mapper.Map<Admin>(adminRequest));
        return Created($"{this.GetUri()}/{result}", result);
    }

    [AuthorizeByRole]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult RemoveAdminById(int id)
    {
        _adminsService.RemoveOrRestoreById(id, true);
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpPatch("{id}/restore")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult RestoreAdminById(int id)
    {
        _adminsService.RemoveOrRestoreById(id, false);
        return NoContent();
    }
}
