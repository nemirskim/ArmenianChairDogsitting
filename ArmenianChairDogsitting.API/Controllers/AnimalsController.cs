using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : Controller
{
    [HttpPost]
    public void Add()
    {

    }

    [HttpGet("{id}")]
    public Animal GetById(int id)
    {
        return new Animal();
    }

    [HttpGet]
    public List<Animal> GetAll()
    {
        return new List<Animal>();
    }

    [HttpPut("{id}")]
    public void UpdateById(int id)
    {

    }

    [HttpDelete("{id}")]
    public void DeleteById(int id)
    {

    }
}
