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
public class ClientsController : Controller
{
    private readonly IClientsService _clientsService;
    private readonly IMapper _mapper;
    public ClientsController(IClientsService clientsService, IMapper mapper)

    {
        _clientsService = clientsService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddClient([FromBody] ClientRegistrationRequest request)
    {
        var result = _clientsService.AddClient(_mapper.Map<Client>(request));
        return Created($"{this.GetUri()}/{result}", result);
    }

    [AuthorizeByRole(Role.Client)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClientMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<ClientMainInfoResponse> GetClientById(int id)
    {
        var result = _clientsService.GetClientById(id);
        if (result is null)
            return NotFound();
        else
            return Ok(_mapper.Map<ClientMainInfoResponse>(result));
    }

    [AuthorizeByRole]
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<ClientAllInfoResponse>> GetAllClients()
    {
        var clients = _clientsService.GetAllClients();
        return Ok(_mapper.Map<List<ClientAllInfoResponse>>(clients));
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPut]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult UpdateClient([FromBody] ClientUpdateRequest request)
    {
        var userId = this.GetUserId(); 
        _clientsService.UpdateClient(_mapper.Map<Client>(request), userId.Value);
        return NoContent();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RemoveClient()
    {
        var userId = this.GetUserId();
        _clientsService.RemoveOrRestoreClient(userId.Value, true);
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpPatch("{id}/restore")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RestoreClient(int id)
    {
        _clientsService.RemoveOrRestoreClient(id, false);
        return NoContent();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePasswordClient([FromBody] UserUpdatePasswordRequest sitterPasswordForUpdate)
    {
        var userId = this.GetUserId();
        _clientsService.UpdatePassword(userId.Value, _mapper.Map<User>(sitterPasswordForUpdate));
        
        return NoContent();
    }
}