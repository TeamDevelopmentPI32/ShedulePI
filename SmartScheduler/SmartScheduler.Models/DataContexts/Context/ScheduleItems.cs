using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface IScheduleItemsDbContext
    {
        ScheduleItem GetScheduleItem(int id);
        IEnumerable<ScheduleItem> GetAllShceduleItems();
        IEnumerable<ScheduleItem> GetAllShceduleItems(Func<DbScheduleItem,bool> predicat);
        IEnumerable<ScheduleItem> GetStudentScheduleItems(int studentId);
        IEnumerable<ScheduleItem> GetTeacherScheduleItems(int teacherId);
        IEnumerable<ScheduleItem> GetAuditoryScheduleItems(int auditoryId);
        IEnumerable<ScheduleItem> GetSubjectScheduleItems(int subjectId);
    }

    public class ScheduleItems : IScheduleItemsDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public ScheduleItems(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<ScheduleItem> GetAllShceduleItems()
        {
           return Context.ScheduleItems.Select(x => x.Convert());
        }

        public IEnumerable<ScheduleItem> GetAllShceduleItems(Func<DbScheduleItem, bool> predicat)
        {
            return Context.ScheduleItems.Where(x=> predicat(x)).Select(x => x.Convert());
        }

        public IEnumerable<ScheduleItem> GetAuditoryScheduleItems(int auditoryId)
        {
            return Context.ScheduleItems.Where(x => x.AuditoryId == auditoryId).Select(x => x.Convert());
        }

        public ScheduleItem GetScheduleItem(int id)
        {
            var item = Context.ScheduleItems.FirstOrDefault(x => x.ScheduleItemId == id);

            if (item == null) return null;

            return item.Convert();
        }

        public IEnumerable<ScheduleItem> GetStudentScheduleItems(int studentId)
        {
            return Context.ScheduleItems.Where(x => x.Group.StudentsInGroups.Any(y => y.StudentId == studentId)).Select(x => x.Convert());
        }

        public IEnumerable<ScheduleItem> GetSubjectScheduleItems(int subjectId)
        {
            return Context.ScheduleItems.Where(x => x.SubjectId == subjectId).Select(x => x.Convert());
        }

        public IEnumerable<ScheduleItem> GetTeacherScheduleItems(int teacherId)
        {
            return Context.ScheduleItems.Where(x => x.TeacherId == teacherId).Select(x => x.Convert());
        }
    }

}
