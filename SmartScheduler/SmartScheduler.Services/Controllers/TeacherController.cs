using Newtonsoft.Json.Linq;
using SmartScheduler.Models.DataContexts.Context;
using SmartScheduler.Models.Models;
using SmartScheduler.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartScheduler.Services.Controllers
{
    [RoutePrefix("api/students")]
    public class TeacherController : ApiController
    {

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllTeacher()
        {
            return Json(SmartSchedulerContext.Instance.Teachers.AllTeachers.ToList());
        }

        //Добавити викладача
        //Ok(200) - викладач доданий
        //BadRequest(400) - неправильний формат запиту
        [RequireAuthorization(Role = "admin")]
        [Route("Add")]
        [HttpPut]
        public IHttpActionResult AddTeacher(JObject teacher)
        {
            if (teacher == null)
                return BadRequest(String.Format("This method should receive json (login, password, name, surname, middleName, enteringYear, rankName)." +
                    "Received: {0}", teacher.ToString()));

            string login = teacher["login"].ToString();
            string pass = teacher["password"].ToString();
            string name = teacher["name"].ToString();
            string surname = teacher["surname"].ToString();
            string middleName = teacher["middleName"].ToString();
            string rankName = teacher["rankName"].ToString();
            int entryYear;

            if (login == null || pass == null || name == null || surname == null || middleName == null || rankName == null)
                return BadRequest(String.Format("This method should receive json (login, password, name, surname, middleName, enteringYear, rankName)." +
                    "Received: {0}", teacher.ToString()));

            try
            {
                entryYear = teacher["enteringYear"].ToObject<int>();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(String.Format("This method should receive json (login, password, name, surname, middleName, enteringYear, rankName)." +
                            "Received: {0}", teacher.ToString()));
            }

            var ranks = SmartSchedulerContext.Instance.TeacherRanks.Ranks.Where(x => x.Name == rankName).ToList();
            if (ranks == null)
                return BadRequest(String.Format("Not found rankName: {0} in ranks. This method should receive json (login, password, name, surname, middleName, enteringYear, rankName)." +
                            "Received: {1}", rankName, teacher.ToString()));

            int res = SmartSchedulerContext.Instance.Teachers.AddTeacher(login, pass, name, surname, middleName, entryYear, ranks[0].Id);

            if (res == -1)
                return BadRequest(String.Format("Something wrong in AddTeacher method (return -1)." +
                    "This method should receive json (login, password, name, surname, middleName, enteringYear, rankName), Received: {0}", teacher.ToString()));

            return Ok();
        }

        //Отримати інформацію про викладача
        //Ok(200) 
        //BadRequest(400) - неправильний формат запиту
        //NotFound() - студент не знайдений у базі даних
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetTeacher(JObject teacher)
        {
            try
            {
                var teach = GetTeacherFromDB(teacher);
                if (teach == null)
                    return BadRequest(String.Format("This method should receive json (login, password)." +
                        "Received: {0}", teacher.ToString()));

                if (teach == null)
                    return NotFound();

                return Ok(teach);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Видалити викладача
        //Ok(200) 
        //BadRequest(400) - неправильний формат запиту
        //NotFound() - студент не знайдений у базі даних
        [RequireAuthorization(Role = "admin")]
        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult DeleteTeacher(JObject teacher)
        {
            Teacher teach;

            try
            {
                teach = GetTeacherFromDB(teacher);
                if (teach == null)
                    return BadRequest(String.Format("This method should receive json (login, password)." +
                        "Received: {0}", teacher.ToString()));

                if (teach == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            bool res = SmartSchedulerContext.Instance.Teachers.DeleteTeacher(teach.Id);

            if (res == false)
                return BadRequest(String.Format("Something wrong in DeleteStudent method (return false). StudentId: {0}" +
                    "This method should receive json (login, password). Received: {1}", teach.Id, teacher.ToString()));

            return Ok();
        }


        private Teacher GetTeacherFromDB(JObject teacher)
        {
            if (teacher == null)
                throw new ArgumentException("teacher == null");

            string login = teacher["login"].ToString();
            string pass = teacher["password"].ToString();

            if (login == null || pass == null)
                throw new ArgumentException(String.Format("This method should receive json (login, password)." +
                    "Received: {0}", teacher.ToString()));

            return SmartSchedulerContext.Instance.Teachers.GetTeacher(login, pass);
        }
    }
}
