using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SmartScheduler.Models.DataContexts.Context;
using Newtonsoft.Json.Linq;
using SmartScheduler.Services.Filters;

namespace SmartScheduler.Services.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            return Json(SmartSchedulerContext.Instance.Students.AllStudents.ToList());
        }

        //Добавити студента
        //Ok(200) - студент доданий
        //BadRequest(400) - неправильний формат запиту
        [RequireAuthorization(Role = "admin")]
        [Route("Add")]
        [HttpPut]
        public IHttpActionResult AddStudent(JObject student)
        {
            if (student == null)
                return BadRequest(String.Format("This method should receive json (login, password, name, surname, middleName, enteringYear)." +
                    "Received: {0}", student.ToString()));

            string login = student["login"].ToString();
            string pass = student["password"].ToString();
            string name = student["name"].ToString();
            string surname = student["surname"].ToString();
            string middleName = student["middleName"].ToString();
            int entryYear;

            if (login == null || pass == null || name == null || surname == null || middleName == null)
                return BadRequest(String.Format("This method should receive json (login, password, name, surname, middleName, enteringYear)." +
                    "Received: {0}", student.ToString()));

            try
            {
                entryYear = student["enteringYear"].ToObject<int>();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(String.Format("This method should receive json (login, password, name, surname, middleName, enteringYear)." +
                            "Received: {0}", student.ToString()));
            }

            int res = SmartSchedulerContext.Instance.Students.AddStudent(login, pass, name, surname, middleName, entryYear);

            if (res == -1)
                return BadRequest(String.Format("Something wrong in AddStudent method (return -1)." +
                    "This method should receive json (login, password, name, surname, middleName, enteringYear), Received: {0}", student.ToString()));

            return Ok();
        }

        //Видалити студента
        //Ok(200) - студент видалений
        //BadRequest(400) - неправильний формат запиту
        //NotFound() - студент не знайдений у базі даних
        [RequireAuthorization(Role = "admin")]
        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult DeleteStudent(JObject student)
        {
            Student stud;

            try
            {
                stud = GetStudentFromDB(student);
                if (stud == null)
                    return BadRequest(String.Format("This method should receive json (login, password)." +
                        "Received: {0}", student.ToString()));

                if (stud == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            bool res = SmartSchedulerContext.Instance.Students.DeleteStudent(stud.Id);

            if (res == false)
                return BadRequest(String.Format("Something wrong in DeleteStudent method (return false). StudentId: {0}" +
                    "This method should receive json (login, password). Received: {1}", stud.Id, student.ToString()));

            return Ok();
        }

        //Отримати інформацію про студента
        //Ok(200)
        //BadRequest(400) - неправильний формат запиту
        //NotFound() - студент не знайдений у базі даних
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetStudent(JObject student)
        {
            try
            {
                var stud = GetStudentFromDB(student);
                if (stud == null)
                    return BadRequest(String.Format("This method should receive json (login, password)." +
                        "Received: {0}", student.ToString()));

                if (stud == null)
                    return NotFound();

                return Ok(stud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Student GetStudentFromDB(JObject student)
        {
            if (student == null)
                throw new ArgumentException("student == null");

            string login = student["login"].ToString();
            string pass = student["password"].ToString();

            if (login == null || pass == null)
                throw new ArgumentException(String.Format("This method should receive json (login, password)." +
                    "Received: {0}", student.ToString()));

            return SmartSchedulerContext.Instance.Students.GetStudent(login, pass);
        }
    }
}
