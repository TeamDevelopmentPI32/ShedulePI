using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface IStudentsDbContext
    {
        int AddStudent(string login, string password, string fname, string lname, string mname, int entryYear);
        bool AssignUserAccount(int StudentId, int UserId);
        bool DeleteStudent(int id, bool withUSer = true);
        bool EditStudent(int id, string newLogin, string newPassword, string fname, string lname, string mname, int entryYear);
        Student GetStudent(int id, bool withUser = false);
        Student GetStudent(string login, string password);
        IEnumerable<Student> AllStudents { get; }
    }

    public class Students : IStudentsDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Students(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Student> AllStudents
        {
            get
            {
                return Context.Students.ToList().Select(x => x.Convert(null));
            }
        }

        public int AddStudent(string login, string password, string fname, string lname, string mname, int entryYear)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return -1;

            if (Context.Users.Any(x => x.Login == login)) return -1;

            var newUser = new DbUser();
            newUser.RegDate = DateTime.Now;
            newUser.Login = login;
            newUser.Password = password;
            Context.Users.Add(newUser);

            var student = new DbStudent();
            student.UserId = newUser.UserId;
            student.Name = fname;
            student.Surname = lname;
            student.Middlename = mname;
            student.EnteringYear = entryYear;
            Context.Students.Add(student);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return student.StudentId;
        }

        public bool AssignUserAccount(int StudentId, int UserId)
        {
            var student = Context.Students.FirstOrDefault(x => x.StudentId == StudentId);
            var user = Context.Users.FirstOrDefault(x => x.UserId == UserId);
            if (student == null || user == null) return false;
            student.User = user;

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

        public bool DeleteStudent(int id, bool withUser = true)
        {
            var student = Context.Students.FirstOrDefault(x => x.StudentId == id);
            if (student == null) return false;

            Context.Students.Remove(student);

            if (withUser)
            {
                Context.Users.Remove(student.User);
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

        public bool EditStudent(int id, string newLogin, string newPassword, string fname, string lname, string mname, int entryYear)
        {
            var student = Context.Students.FirstOrDefault(x => x.StudentId == id);
            
            if (student == null) return false;

            student.Name = fname;
            student.Surname = lname;
            student.Middlename = mname;
            student.EnteringYear = entryYear;

            student.User.Login = newLogin;
            student.User.Password = newPassword;

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

        public Student GetStudent(int id, bool withUser = false)
        {
            var student = Context.Students.FirstOrDefault(x => x.StudentId == id);

            if (student == null) return null;

            return student.Convert(withUser? student.User : null);
        }

        public Student GetStudent(string login, string password)
        {
            var student = Context.Students.FirstOrDefault(x => x.User.Login == login && x.User.Password == password);

            if (student == null) return null;

            return student.Convert(student.User);
        }
    }

       


}
