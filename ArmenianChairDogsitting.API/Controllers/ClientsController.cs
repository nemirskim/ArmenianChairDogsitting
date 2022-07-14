using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class ClientsController : Controller
{
    private readonly IClientsRepository _clientsRepository;
    public ClientsController(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddClient([FromBody] ClientRegistrationRequest request)
    {
        int id = 23;
        return Created($"{this.GetUri()}/{id}", id);
    }

    [AuthorizeByRole(Role.Client)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClientAllInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<ClientAllInfoResponse> GetClientById(int id)
    {
        var result = _clientsRepository.GetClientById(id);
        if (result is null)
            return NotFound();
        else
            return Ok(result);
    }

    [AuthorizeByRole]
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<ClientAllInfoResponse>> GetAllClients()
    {
        var clients = _clientsRepository.GetAllClients();
        return Ok(clients);
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult UpdateClient([FromBody] ClientUpdateRequest request, int id)
    {
        var client = _clientsRepository.GetClientById(id);
        _clientsRepository.UpdateClient(client);
        return Ok();
    }

    [AuthorizeByRole(Role.Client)]
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RemoveClient(int id)
    {
        _clientsRepository.RemoveOrRestoreClient(id, true);
        return NoContent();
    }

    [AuthorizeByRole]
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RestoreClient(int id)
    {
        _clientsRepository.RemoveOrRestoreClient(id, false);
        return NoContent();
    }
}
