using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class ScheduleRequest
{
    [Required(ErrorMessage = ApiErrorMessage.StartTimeIsRequired)]
    public DateTime Start { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.EndTimeIsRequired)]
    public DateTime End { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.WeekDayIsRequired)]
    [EnumRange<DayOfWeek>]
    public DayOfWeek Day { get; set; }
}
