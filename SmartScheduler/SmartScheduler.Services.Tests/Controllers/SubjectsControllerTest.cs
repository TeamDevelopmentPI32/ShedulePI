using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartScheduler.Services.Controllers;
using System.Collections.Generic;

namespace SmartScheduler.Services.Tests.Controllers
{
    [TestClass]
    public class SubjectsControllerTest
    {
        [TestMethod]
        public void GetAllSubjects()
        {
            SubjectsController controller = new SubjectsController();
            List<SmartScheduler.Models.Models.Subject> asd = controller.GetAllSubjects();

        }

        [TestMethod]
        public void GetSubjectById()
        {
        }

        [TestMethod]
        public void GetSubjectsById()
        {
        }

        [TestMethod]
        public void GetSubjectsWithCredits()
        {
        }

        [TestMethod]
        public void GetConcreateSubject()
        {
        }

        [TestMethod]
        public void AddSubject()
        {
        }

        [TestMethod]
        public void DeleteSubject()
        {
        }
    }
}
