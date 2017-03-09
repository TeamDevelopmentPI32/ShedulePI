using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface ISchedulesDbContext
    {
        IEnumerable<Schedule> AllSchedules { get; }
        int AddSchedule(int number, DateTime date);
        bool DeleteSchedule(int id);
    }
    public class Schedules : ISchedulesDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Schedules(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Schedule> AllSchedules
        {
            get
            {
                return Context.Schedules.ToList().Select(x=>x.Convert());
            }
        }

        public int AddSchedule(int number, DateTime date)
        {
            var item = Context.Schedules.FirstOrDefault(x=>x.Number == number && x.Date == date);
            if (item != null)
            {
                return item.ScheduleId;
            }
            item = new DbSchedule() { Number = number, Date = date };

            Context.Schedules.Add(item);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return item.ScheduleId;
        }

        public bool DeleteSchedule(int id)
        {
            var item = Context.Schedules.FirstOrDefault(x => x.ScheduleId == id);
            if (item == null) return false;

            Context.Schedules.Remove(item);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return false;
            }
            return true;
        }
    }

}
