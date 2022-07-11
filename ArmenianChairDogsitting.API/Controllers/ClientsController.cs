using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Roles;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class ClientsController : Controller
{
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddClient([FromBody] ClientRegistrationRequest request)
    {
        int id = 23;
        return Created($"{this.GetUri()}/{id}", id);
    }

    [AuthorizeByRole(Role.Client, Role.Admin)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClientAllInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientAllInfoResponse> GetClientById(int id)
    {
        return Ok(new ClientAllInfoResponse());
    }


    [AuthorizeByRole(Role.Admin)]
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<ClientAllInfoResponse>> GetAllClients()
    {
        return Ok(new List<ClientAllInfoResponse>());
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientAllInfoResponse> UpdateClient([FromBody] ClientUpdateRequest request, int id)
    {
        return Ok(new ClientAllInfoResponse());
    }

    [AuthorizeByRole(Role.Client)]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult RemoveClient(int id)
    {
        return NoContent();
    }

    [AuthorizeByRole(Role.Admin)]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeactivateClientById(int id)
    {
        return NoContent();
    }

}
