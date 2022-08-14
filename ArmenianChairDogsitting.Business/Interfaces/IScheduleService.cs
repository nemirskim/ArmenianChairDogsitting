

using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface IScheduleService
{
    public int AddWorkTime(Schedule workTime, int id);
    public void RemoveWorkTime(int userId, int id);
    public List<Schedule> GetSitterSchedule (int id);
}
