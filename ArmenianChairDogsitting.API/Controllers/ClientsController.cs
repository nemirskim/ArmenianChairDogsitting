using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API;

namespace ArmenianChairDogsitting.API.Controllers
{
    public class ClientsController : Controller
    {
        [HttpPost]
        public void AddClient()
        {
            
        }

        [HttpGet("{id}")]
        public Client GetClientById(int id)
        {
            return new Client();
        }

        [HttpGet]
        public List<Client> GetAllClients()
        {
            return new List<Client>();
        }

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
}
