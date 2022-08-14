

using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories.Interfaces;

public interface IScheduleRepository
{
    public int AddWorkTime(Schedule workTime);
    public void RemoveWorkTime(Schedule workTime);
    public Schedule GetScheduleById(int id);
}
