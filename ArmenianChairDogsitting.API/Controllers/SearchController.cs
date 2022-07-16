using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Extensions;
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
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<List<SitterAllInfoResponse>> GetSitters([FromBody] SearchRequest searchOptions)
    {
        var result = _searchService.GetSitters(_mapper.Map<Search>(searchOptions));
        return _mapper.Map<List<SitterAllInfoResponse>>(result);
    }
}
