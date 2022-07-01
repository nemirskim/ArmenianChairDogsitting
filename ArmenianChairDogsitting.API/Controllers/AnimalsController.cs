using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Roles;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class AnimalsController : Controller
{

    [Authorize(Roles = nameof(Role.Client))]
    [HttpPost]
    public void Add( [FromBody] AnimalAddRequest animal)
    {

    }

    [HttpGet("{id}")]
    public AnimalGetByIdResponse GetById(int id)
    {
        return new AnimalGetByIdResponse();
    }

    [HttpGet]
    public List<AnimalGetAllResponse> GetAll()
    {
        return new List<AnimalGetAllResponse>();
    }

    [Authorize(Roles = nameof(Role.Client))]
    [HttpPut("{id}")]
    public void UpdateById(int id)
    {

    }

    [Authorize(Roles = nameof(Role.Client))]
    [HttpDelete("{id}")]
    public void DeleteById(int id)
    {

    }
}
