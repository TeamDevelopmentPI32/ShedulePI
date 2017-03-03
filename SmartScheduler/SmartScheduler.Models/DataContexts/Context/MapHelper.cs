using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public static class MapHelper
    {
        public static User Convert(this DbUser user)
        {
            return new User()
            {
                UserId = user.UserId,
                LastActivityDate = user.LastActivityDate??DateTime.Now,
                Login = user.Login,
                Password = user.Password,
                RegistredDate = user.RegDate
            };
        }

        public static Student Convert(this DbStudent student, DbUser user = null)
        {
            var res = new Student()
            {
                Id = student.StudentId,
                EntryYear = student.EnteringYear,
                Name = student.Name,
                Surname = student.Surname,
                Middlename = student.Middlename,
            };
            if (user != null)
            {
                res.Login = user.Login;
                res.UserId = user.UserId;
                res.Password = user.Password;
                res.LastActivityDate = user.LastActivityDate??DateTime.Now;
                res.RegistredDate = user.RegDate;
            }
            return res;
        }

        public static Teacher Convert(this DbTeacher teacher, DbUser user = null)
        {
            var res = new Teacher()
            {
                Id = teacher.TeacherId,
                EntryYear = teacher.EnteringYear,
                Name = teacher.Name,
                Surname = teacher.Surname,
                Middlename = teacher.Middlename,
                Rank = new TeacherRank()
                {
                    Id = teacher.Rank.RankId,
                    Name = teacher.Rank.RankName
                }
            };
            if (user != null)
            {
                res.Login = user.Login;
                res.UserId = user.UserId;
                res.Password = user.Password;
                res.LastActivityDate = user.LastActivityDate ?? DateTime.Now;
                res.RegistredDate = user.RegDate;
            }
            return res;
        }

        public static Administrator Convert(this DbAdministator administrator, DbUser user = null)
        {
            var res = new Administrator()
            {
                Id = administrator.AdministratorId,
                Name = administrator.Name,
                Surname = administrator.Surname,
            };
            if (user != null)
            {
                res.Login = user.Login;
                res.UserId = user.UserId;
                res.Password = user.Password;
                res.LastActivityDate = user.LastActivityDate ?? DateTime.Now;
                res.RegistredDate = user.RegDate;
            }
            return res;
        }

        public static TeacherRank Convert(this DbRank rank)
        {
            return new TeacherRank() { Id = rank.RankId, Name = rank.RankName };
        }

        public static Group Convert(this DbGroup group)
        {
            return new Group() { Id = group.GroupId, Name = group.Name };
        }

        public static Subject Convert(this DbSubject subject)
        {
            return new Subject() { Id = subject.SubjectId, Name = subject.Name, Credits = subject.Credits };
        }

        public static Auditory Convert(this DbAuditory auditory)
        {
            return new Auditory() { Id = auditory.AuditoryId, Number = auditory.Number };
        }

        public static Schedule Convert(this DbSchedule schedule)
        {
            return new Schedule() { Id = schedule.ScheduleId, Number = schedule.Number??-1, Date = schedule.Date??DateTime.Now };
        }

        public static ScheduleItem Convert(this DbScheduleItem scheduleItem)
        {
            return new ScheduleItem()
            {
                Auditory = scheduleItem.Auditory.Convert(),
                Group = scheduleItem.Group.Convert(),
                Id = scheduleItem.ScheduleItemId,
                NumberLeson = scheduleItem.ScheduleYear,
                Schedule = scheduleItem.Schedule.Convert(),
                Subjcect = scheduleItem.Subject.Convert(),
                Teacher = scheduleItem.Teacher.Convert()
            };
        }
    }
}
