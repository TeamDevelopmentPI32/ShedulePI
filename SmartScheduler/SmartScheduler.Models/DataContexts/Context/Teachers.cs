using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface ITeachersDbContext
    {
        int AddTeacher(string login, string password, string fname, string lname, string mname, int entryYear, int rankId);
        bool AssignUserAccount(int TeacherId, int UserId);
        bool DeleteTeacher(int id, bool withUSer = true);
        bool EditTeacher(int id, string newLogin, string newPassword, string fname, string lname, string mname, int entryYear, int rankId);
        Teacher GetTeacher(int id, bool withUser = false);
        Teacher GetTeacher(string login, string password);
        IEnumerable<Teacher> AllTeachers { get; }
    }

    public class Teachers : ITeachersDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Teachers(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Teacher> AllTeachers
        {
            get
            {
                return Context.Teachers.ToList().Select(x => x.Convert(null));
            }
        }

        public int AddTeacher(string login, string password, string fname, string lname, string mname, int entryYear, int rankId)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return -1;

            if (Context.Users.Any(x => x.Login == login)) return -1;

            var newUser = new DbUser();
            newUser.RegDate = DateTime.Now;
            newUser.Login = login;
            newUser.Password = password;
            Context.Users.Add(newUser);

            var Teacher = new DbTeacher();
            Teacher.UserId = newUser.UserId;
            Teacher.Name = fname;
            Teacher.Surname = lname;
            Teacher.Middlename = mname;
            Teacher.EnteringYear = entryYear;
            Teacher.RankId = rankId;
            Context.Teachers.Add(Teacher);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return Teacher.TeacherId;
        }

        public bool AssignUserAccount(int TeacherId, int UserId)
        {
            var Teacher = Context.Teachers.FirstOrDefault(x => x.TeacherId == TeacherId);
            var user = Context.Users.FirstOrDefault(x => x.UserId == UserId);
            if (Teacher == null || user == null) return false;
            Teacher.User = user;

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

        public bool DeleteTeacher(int id, bool withUser = true)
        {
            var Teacher = Context.Teachers.FirstOrDefault(x => x.TeacherId == id);
            if (Teacher == null) return false;

            Context.Teachers.Remove(Teacher);

            if (withUser)
            {
                Context.Users.Remove(Teacher.User);
            }

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

        public bool EditTeacher(int id, string newLogin, string newPassword, string fname, string lname, string mname, int entryYear, int rankId)
        {
            var Teacher = Context.Teachers.FirstOrDefault(x => x.TeacherId == id);
            
            if (Teacher == null) return false;

            Teacher.Name = fname;
            Teacher.Surname = lname;
            Teacher.Middlename = mname;
            Teacher.EnteringYear = entryYear;
            Teacher.RankId = rankId;

            Teacher.User.Login = newLogin;
            Teacher.User.Password = newPassword;

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

        public Teacher GetTeacher(int id, bool withUser = false)
        {
            var Teacher = Context.Teachers.FirstOrDefault(x => x.TeacherId == id);

            if (Teacher == null) return null;

            return Teacher.Convert(withUser? Teacher.User : null);
        }

        public Teacher GetTeacher(string login, string password)
        {
            var Teacher = Context.Teachers.FirstOrDefault(x => x.User.Login == login && x.User.Password == password);

            if (Teacher == null) return null;

            return Teacher.Convert(Teacher.User);
        }
    }

       


}
