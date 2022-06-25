using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers
{
    public class SittersController : Controller
    {
        [HttpPost]
        public void AddSitter()
        {

        }

        [HttpGet("{id}")]
        public Sitter GetSitterById(int id)
        {
            return Sitter;
        }

        [HttpGet]
        public List<Sitter> GetAllSitters()
        {
            return List<Sitter>;
        }

        [HttpPut("{id}")]
        public void UpdateSitterById(int id)
        {

        }

        [HttpDelete("{id}")]
        public void RemoveSitterById(int id)
        {

        }

        [HttpPatch("{id}")]
        public DeactivateSitterById(int id)
        {

        }
    }
}
