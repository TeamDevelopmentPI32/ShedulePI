using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartScheduler.Models.Helpers;

namespace SmartScheduler.Models.Tests
{
    [TestClass]
    public class SessionHelperTests
    {
        [TestMethod]
        public void AddSession_SessionAdded_True()
        {
            var id = 7;
            var token = SessionHelper.CreateSession(id);

            Assert.AreEqual(SessionHelper.GetSession(token), id);
            Assert.IsTrue(SessionHelper.ClearSession(token));
            Assert.AreEqual(SessionHelper.GetSession(token), -1);
            Assert.IsFalse(SessionHelper.ClearSession(token));
        }
    }
}
