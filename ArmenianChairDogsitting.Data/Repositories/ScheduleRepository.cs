
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;

namespace ArmenianChairDogsitting.Data.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public ScheduleRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public int AddWorkTime(Schedule workTime)
    {
        _context.Schedules.Add(workTime);
        _context.SaveChanges();
        return workTime.Id;
    }

    public Schedule GetScheduleById(int id) => _context.Schedules.FirstOrDefault(sc => sc.Id == id);
}
