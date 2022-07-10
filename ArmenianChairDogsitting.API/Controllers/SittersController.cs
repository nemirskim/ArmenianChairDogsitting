using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SittersController : Controller
{

    private readonly ISitterRepository _sittersRepository;

    public SittersController(ISitterRepository sittersRepository)
    {
        _sittersRepository = sittersRepository;
    }

    public SittersController()
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddSitter([FromBody] SitterRequest sitterRequest)
    {
        /* var sitter = new Sitter
         {
             Name = sitterRequest.Name,
             LastName = sitterRequest.LastName,
             Phone = sitterRequest.Phone,
             Email = sitterRequest.Email,
             Password = sitterRequest.Password,
             Age = sitterRequest.Age,
             Experience = sitterRequest.Experience,
             Sex = sitterRequest.Sex,
             Description = sitterRequest.Description,
             PricesCatalog = sitterRequest.PriceCatalog
         };

         var result = _sittersRepository.Add(sitter);*/

        int result = 1;
        return Created($"{this.GetUri()}/{result}", result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SitterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SitterMainInfoResponse> GetSitterById(int id)
    {
        //var result = _sittersRepository.GetById(id);

        /*if (result is null)
            return NotFound();
        else
            return Ok(result);*/

        return Ok(new SitterMainInfoResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SitterAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<SitterAllInfoResponse>> GetAllSitters()
    {
        //var sitters = _sittersRepository.GetSitters();
        //return Ok(sitters);
        return Ok(new List<SitterAllInfoResponse>());
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdateSitter(SitterUpdateRequest sitterUpdateRequest, int id)
    {
        /*var sitter = new Sitter
        {
            Name = sitterUpdateRequest.Name,
            LastName = sitterUpdateRequest.LastName,
            Phone = sitterUpdateRequest.Phone,
            Age = sitterUpdateRequest.Age,
            Experience = sitterUpdateRequest.Experience,
            Sex = sitterUpdateRequest.Sex,
            Description = sitterUpdateRequest.Description,
        };

        _sittersRepository.Update(sitter, id);*/

        return NoContent();
    }

    [AuthorizeByRole]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult RemoveOrRestoreSitterById(int id)
    {
        //_sittersRepository.RemoveOrRestoreById(id);

        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}/{password}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePasswordSitter(int id, string password)
    {
        //_sittersRepository.UpdatePassword(id, password);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpPatch("{id}/{catalog}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdatePriceCatalogSitter(int id, List<PriceCatalog> priceCatalog)
    {
       //_sittersRepository.UpdatePriceCatalog(id, priceCatalog);
        return NoContent();
    }

    [AuthorizeByRole(Role.Sitter)]
    [HttpGet("{id}/Schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult GetAllSittersWithWorkTimes(int id)
    {
        return NoContent();
    }
}
