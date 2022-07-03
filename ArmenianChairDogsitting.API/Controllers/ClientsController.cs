using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController : Controller
{
    [HttpPost]
    public void AddClient()
    {
        
    }

    //[HttpGet("{id}")]
    //public Client GetClientById(int id)
    //{
    //    return new Client();
    //}

    //[HttpGet]
    //public List<Client> GetAllClients()
    //{
    //    return new List<Client>();
    //}

    [HttpPut("{id}")]
    public void UpdateClientById(int id)
    {

    }

    [HttpDelete]
    public void RemoveClient(int id)
    {

    }

    [HttpPatch("{id}")]
    public void DeactivateClientById(int id)
    {

    }

}
