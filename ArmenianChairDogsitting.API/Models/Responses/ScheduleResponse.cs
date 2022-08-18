namespace ArmenianChairDogsitting.API.Models;

public class ScheduleResponse
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DayOfWeek Day { get; set; }
}
