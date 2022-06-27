using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AreaController : Controller
{
    [HttpPost]
    public void Add()
    {

    }

    [HttpGet("{id}")]
    public Area GetById(int id)
    {
        return new Area();
    }

    [HttpGet]
    public List<Area> GetAll()
    {
        return new List<Area>();
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
