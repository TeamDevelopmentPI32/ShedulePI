using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartScheduler.Services.Controllers;

namespace SmartScheduler.Services.Tests.Controllers
{
    [TestClass]
    public class GroupsControllerTest
    {
        [TestMethod]
        public void GetAllGroups()
        {
            var controller = new GroupsController();
            var temp = controller.GetAllGroups();
        }

        [TestMethod]
        public void GetGroupByName()
        {
            var controller = new GroupsController();
            var temp = controller.GetGroupByName("PI-32");
        }

        [TestMethod]
        public void GetGroupById()
        {
            var controller = new GroupsController();
            var temp = controller.GetGroupById(0);
        }

        [TestMethod]
        public void AddGroup()
        {
            var controller = new GroupsController();
            var temp = controller.AddGroup("PI-42");
        }

        [TestMethod]
        public void DeleteGroup()
        {
            var controller = new GroupsController();
            var temp = controller.DeleteGroup("PI-42");
        }
    }
}
