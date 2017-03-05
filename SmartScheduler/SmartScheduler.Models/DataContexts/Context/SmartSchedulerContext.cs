using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface ISmartSchedulerDbContext
    {
        IAdministratorsDbContext Administrators { get; set; }
        IAuditoriesDbContext Auditories { get; set; }
        IGroupsDbContext Groups { get; set; }
        IScheduleItemsDbContext ScheduleItems { get; set; }
        ISchedulesDbContext Schedules { get; set; }
        IStudentsDbContext Students { get; set; }
        ISubjectsDbContext Subjects { get; set; }
        ITeacherRanksDbContext TeacherRanks { get; set; }
        ITeachersDbContext Teachers { get; set; }
        IUsersDbContext Users { get; set; }
    }

    public class SmartSchedulerContext: ISmartSchedulerDbContext
    {
        private SmartSchedulerEntities m_Context = new SmartSchedulerEntities();

        private static ISmartSchedulerDbContext m_Instance;
        public static ISmartSchedulerDbContext Instance
        {
            get
            {
                return m_Instance ?? (m_Instance = new SmartSchedulerContext());
            }
        }

        private SmartSchedulerContext()
        {
            try
            {
                m_Context.Database.Exists();
            }
            catch (Exception ex)
            {
                throw new Exception("No data base connection!!! Check connection string in app settings or check MS SQL process!");
            }
            
            Administrators = new Administrators(m_Context);
            Auditories = new Auditories(m_Context);
            ScheduleItems = new ScheduleItems(m_Context);
            Schedules = new Schedules(m_Context);
            Students = new Students(m_Context);
            Subjects = new Subjects(m_Context);
            TeacherRanks = new TeacherRanks(m_Context);
            Teachers = new Teachers(m_Context);
            Users = new Users(m_Context);
        }

        public IAdministratorsDbContext Administrators { get; set; }
        public IAuditoriesDbContext Auditories { get; set; }
        public IGroupsDbContext Groups { get; set; }
        public IScheduleItemsDbContext ScheduleItems { get; set; }
        public ISchedulesDbContext Schedules { get; set; }
        public IStudentsDbContext Students { get; set; }
        public ISubjectsDbContext Subjects { get; set; }
        public ITeacherRanksDbContext TeacherRanks { get; set; }
        public ITeachersDbContext Teachers { get; set; }
        public IUsersDbContext Users { get; set; }
    }


}
