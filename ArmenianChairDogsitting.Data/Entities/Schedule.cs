

namespace ArmenianChairDogsitting.Data.Entities;

public class Schedule
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DayOfWeek Day { get; set; }
    public Sitter Sitter { get; set; }
}
