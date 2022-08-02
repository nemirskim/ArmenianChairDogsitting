using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class SearchController : Controller
{
    ISearchService _searchService;
    IMapper _mapper;
    public SearchController(ISearchService searchService, IMapper mapper)
    {
        _searchService = searchService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SitterAllInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<List<SitterAllInfoResponse>> GetSittersBySearchParams([FromBody] SearchRequest searchOptions)
    {
        var result = _searchService.GetSittersBySearchParams(_mapper.Map<ParamsToSearchSitter>(searchOptions));
        return Ok(_mapper.Map<List<SitterAllInfoResponse>>(result));
    }
}
