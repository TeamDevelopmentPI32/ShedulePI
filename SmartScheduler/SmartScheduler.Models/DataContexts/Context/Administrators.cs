using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface IAdministratorsDbContext
    {
        int AddAdministrator(string login, string password, string fname, string lname, string mname, int entryYear);
        bool AssignUserAccount(int AdministratorId, int UserId);
        bool DeleteAdministrator(int id, bool withUSer = true);
        bool EditAdministrator(int id, string newLogin, string newPassword, string fname, string lname);
        Administrator GetAdministrator(int id, bool withUser = false);
        Administrator GetAdministrator(string login, string password);
        IEnumerable<Administrator> AllAdministrators { get; }
    }

    public class Administrators : IAdministratorsDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Administrators(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Administrator> AllAdministrators
        {
            get
            {
                return Context.Administators.ToList().Select(x => x.Convert(null));
            }
        }

        public int AddAdministrator(string login, string password, string fname, string lname, string mname, int entryYear)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return -1;

            if (Context.Users.Any(x => x.Login == login)) return -1;

            var newUser = new DbUser();
            newUser.RegDate = DateTime.Now;
            newUser.Login = login;
            newUser.Password = password;
            Context.Users.Add(newUser);

            var administrator = new DbAdministator();
            administrator.UserId = newUser.UserId;
            administrator.Name = fname;
            administrator.Surname = lname;
            Context.Administators.Add(administrator);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return administrator.AdministratorId;
        }

        public bool AssignUserAccount(int AdministratorId, int UserId)
        {
            var administrator = Context.Administators.FirstOrDefault(x => x.AdministratorId == AdministratorId);
            var user = Context.Users.FirstOrDefault(x => x.UserId == UserId);
            if (administrator == null || user == null) return false;
            administrator.User = user;

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

        public bool DeleteAdministrator(int id, bool withUser = true)
        {
            var administrator = Context.Administators.FirstOrDefault(x => x.AdministratorId == id);
            if (administrator == null) return false;

            Context.Administators.Remove(administrator);

            if (withUser)
            {
                Context.Users.Remove(administrator.User);
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

        public bool EditAdministrator(int id, string newLogin, string newPassword, string fname, string lname)
        {
            var administrator = Context.Administators.FirstOrDefault(x => x.AdministratorId == id);
            
            if (administrator == null) return false;

            administrator.Name = fname;
            administrator.Surname = lname;

            administrator.User.Login = newLogin;
            administrator.User.Password = newPassword;

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

        public Administrator GetAdministrator(int id, bool withUser = false)
        {
            var administrator = Context.Administators.FirstOrDefault(x => x.AdministratorId == id);

            if (administrator == null) return null;

            return administrator.Convert(withUser? administrator.User : null);
        }

        public Administrator GetAdministrator(string login, string password)
        {
            var administrator = Context.Administators.FirstOrDefault(x => x.User.Login == login && x.User.Password == password);

            if (administrator == null) return null;

            return administrator.Convert(administrator.User);
        }
    }

       


}
