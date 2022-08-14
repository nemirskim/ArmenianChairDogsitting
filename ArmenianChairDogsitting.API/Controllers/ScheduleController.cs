using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;
    private readonly IMapper _mapper;

    public ScheduleController(IScheduleService scheduleService, IMapper mapper)
    {
        _scheduleService = scheduleService;
        _mapper = mapper;
    }

    [HttpPost]
    [AuthorizeByRole(Role.Sitter)]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddWorkTime([FromBody]ScheduleRequest workTime)
    {
        var schedule = _mapper.Map<Schedule>(workTime);
        var userId = this.GetUserId();

        var result = _scheduleService.AddWorkTime(schedule, userId.Value);
        return Created($"{this.GetUri()}/{result}", result);
    }

    [HttpDelete("{id}")]
    [AuthorizeByRole(Role.Sitter)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult RemoveWorkTime(int id)
    {
        var userId = this.GetUserId();

        _scheduleService.RemoveWorkTime(userId.Value, id);
        return NoContent();
    }

    [HttpGet]
    [AuthorizeByRole(Role.Sitter)]
    [ProducesResponseType(typeof(List<ScheduleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<ScheduleResponse>> GetSitterSchedule()
    {
        var userId = this.GetUserId();

        var result = _scheduleService.GetSitterSchedule(userId.Value);
        return Ok(_mapper.Map<Schedule>(result));
    }
}
