

using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;

namespace ArmenianChairDogsitting.Business.Services;

public class ScheduleService : IScheduleService
{
    ISittersRepository _sitterRepository;
    IScheduleRepository _scheduleRepository;

    public ScheduleService(ISittersRepository sitterRepository, IScheduleRepository scheduleRepository)
    {
        _sitterRepository = sitterRepository;
        _scheduleRepository = scheduleRepository;
    }

    public int AddWorkTime(Schedule workTime, int id)
    {
        var sitter = _sitterRepository.GetById(id);
        workTime.Sitter = sitter;
        return _scheduleRepository.AddWorkTime(workTime);
    }

    public List<Schedule> GetSitterSchedule(int id)
    {
        var sitter = _sitterRepository.GetById(id);
        return sitter.Schedules;
    }

    public void RemoveWorkTime(int userId, int id)
    {
        var sitter = _sitterRepository.GetById(userId);
        var schedule = _scheduleRepository.GetScheduleById(id);

        sitter.Schedules.Remove(schedule);
        schedule.Sitter = null;

        _sitterRepository.Update(sitter);
        _scheduleRepository.RemoveWorkTime(schedule);
    }
}
