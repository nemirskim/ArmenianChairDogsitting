using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API;

namespace ArmenianChairDogsitting.API.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class ServicesController : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddService([FromBody] ServiceRequest client)
    {
        int id = 42;
        return Created($"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}/{id}", id);
    }

    [AuthorizeByRole(RoleConstants.Client)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ServiceResponse> GetServiceById(int id)
    {
        return Ok(new ServiceResponse());
    }

    [AuthorizeByRole(RoleConstants.Client)]
    [HttpGet]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<ServiceResponse>> GetAllServices()
    {
        return Ok(new List<ServiceResponse>());
    }

    [AuthorizeByRole(RoleConstants.Manager)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateServiceById([FromBody] ServiceRequest req, int id)
    {
        return Ok();
    }

    [AuthorizeByRole(RoleConstants.Manager)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public ActionResult RemoveServiceById(int id)
    {
        return Ok();
    }

    [AuthorizeByRole(RoleConstants.Client)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<object>> GetSittersWithService([FromBody] int ServiceId)
    {
        return Ok(new List<object>());
    }
}
