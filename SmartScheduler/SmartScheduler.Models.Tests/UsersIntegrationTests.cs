using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartScheduler.Models.DataContexts.Context;
using static SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext;

namespace SmartScheduler.Models.Tests
{
    [TestClass]
    public class UsersIntegrationTests
    {
        [TestMethod]
        public void AddUserCheckUserDeleteUser_UserAdded_True()
        {
            var login = "test_login";
            var passord = "test_password";
            var id = Instance.Users.AddUser(login, passord);
            Assert.AreNotEqual(id, -1);
            var user = Instance.Users.GetUser(login, passord);
            Assert.IsNotNull(user);
            Assert.IsTrue(string.Equals(user.Login, login));
            Assert.IsTrue(string.Equals(user.Password, passord));
            var deleteRes = Instance.Users.DeleteUser(id);
            Assert.IsTrue(deleteRes);
        }
    }
}
