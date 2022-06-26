using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers
{
    public class AnimalsController : Controller
    {
        [HttpPost]
        public void Add()
        {

        }

        [HttpGet("{id}")]
        public Animal GetById(int id)
        {
            returb Animal;
        }

        [HttpGet]
        public List<Animal> GetAll()
        {

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
}
