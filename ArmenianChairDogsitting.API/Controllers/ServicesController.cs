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

    [AuthorizeByRole()]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public void GetServiceById(int id)
    {

    }

    [HttpGet]
    public List<> GetAllServices()
    {

    }

    [HttpPut("{id}")]
    public void UpdateServiceById(int id)
    {

    }

    [HttpDelete("{id}")]
    public void RemoveServiceById(int id)
    {

    }

    [HttpGet]
    public List<> GetSittersWithServices()
    {

    }
}
