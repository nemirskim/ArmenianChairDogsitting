using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers
{
    public class ClientsController : Controller
    {
        [HttpPost]
        public void AddNewClient()
        {
            
        }

        [HttpGet("{id}")]
        public Client GetClientById(int id)
        {
            return Client;
        }

        [HttpGet]
        public List<Client> GetAllClients()
        {
            return List<Client>;
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
        public DeactivateClientById(int id)
        {

        }



    }
}
