using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface IUsersDbContext
    {
        int AddUser(string login, string password);
        bool DeleteUser(int id);
        bool EditUser(int id, string newLogin, string newPassword);
        User GetUser(string login,string password);
        void UpdateLastActivity(int id);
        bool UserExist(string login);
    }

    public class Users : IUsersDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Users(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public bool UserExist(string login)
        {
            return Context.Users.Any(x=> x.Login == login);
        }

        public User GetUser(string login, string password)
        {
            return Context.Users.FirstOrDefault(x=>x.Login == login && x.Password == password).Convert();
        }

        public int AddUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return -1;

            if (Context.Users.Any(x => x.Login == login)) return -1;

            var newUser = new DbUser();
            newUser.RegDate = DateTime.Now;
            newUser.Login = login;
            newUser.Password = password;
            Context.Users.Add(newUser);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return newUser.UserId;
        }

        public bool DeleteUser(int id)
        {
            var user = Context.Users.FirstOrDefault(x=>x.UserId == id);
            if (user == null) return false;

            Context.Users.Remove(user);

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

        public bool EditUser(int id, string newLogin, string newPassword)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null) return false;

            user.Login = newLogin;
            user.Password = newPassword;

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

        public void UpdateLastActivity(int id)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null) return;

            user.LastActivityDate = DateTime.Now;

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
            }
        }
    }


}
