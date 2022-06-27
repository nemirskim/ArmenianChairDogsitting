using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicesController : Controller
{
    [HttpPost]
    public void AddService()
    {

    }

    [HttpGet("{id}")]
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
