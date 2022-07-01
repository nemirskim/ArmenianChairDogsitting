using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SittersController : Controller
{
    [HttpPost]
    public void AddSitter()
    {

    }

    [HttpGet("{id}")]
    public Sitter GetSitterById(int id)
    {
        return new Sitter();           
    }

    [HttpGet]
    public List<Sitter> GetAllSitters()
    {
        return new List<Sitter>();
    }

    [Authorize(Roles = nameof(Role.Sitter))]
    [HttpPut("{id}")]
    public void UpdateSitterById(int id)
    {

    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HttpDelete("{id}")]
    public void RemoveSitterById(int id)
    {

    }


    [HttpGet]
    public void GetAllSettersWithWorkTimes()
    {

    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HttpPatch("{id}")]
    public void DeactivateSitterById(int id)
    {

    }
}
