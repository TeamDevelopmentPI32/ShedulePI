using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartScheduler.Services.Controllers
{

    [RoutePrefix("api/subjects")]
    public class SubjectsController : ApiController
    {
        //Получить все предметы
        [Route("")]
        [HttpGet]
        public List<Subject> GetAllSubjects()
        {
            return SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.AllSubjects.ToList();
        }

        //Получить список предметов с конкретным названием
        [Route("{subjectname: alpha}")]
        [HttpGet]
        public List<Subject> GetSubjectsById(string subjectName)
        {
            var temp = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.AllSubjects;
            var subject =  (from subj in temp
                           where subj.Name == subjectName
                           select subj).ToList();
            return subject;
        }

        //Получить список предметом с конкретным числом кредитов
        [Route("{credits: int}")]
        [HttpGet]
        public List<Subject> GetSubjectsWithCredits(int credits)
        {
            var temp = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.AllSubjects;
            var subject = (from subj in temp
                           where subj.Credits == credits
                           select subj).ToList();
            return subject;
        }

        //Получить конкретный ОДИН предмет
        [Route("concreteSubject")]
        [HttpGet]
        public Subject GetConcreteSubject(string input)
        {
            if (input != null)
            {
                var arguments = input.Split(' ');
                if (arguments.Count() <= 2)
                {
                    var temp = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.AllSubjects;
                    int credits;
                    if (Int32.TryParse(arguments[1], out credits))
                    {
                        var subject = (from subj in temp
                                       where subj.Credits == credits && subj.Name == arguments[0]
                                       select subj).ToList();
                        if (subject.Count == 1)
                        {
                            return subject[0];
                        }
                    }
                }
            }
            return null;    
        }

        //Добавить предмет в БД
        //Ok(200) - если предмет нормально добавился
        //BadRequest(400) - в случаи, если предмет уже есть(да, 400-е сообщение тут не очень подходит логически, но блин...
        [Route("")]
        [HttpPost]
        public IHttpActionResult AddSubject([FromBody]string input)
        {
            if (input != null)
            {
                var arguments = input.Split(' ');
                if (arguments.Count() <= 2)
                {
                    var index = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.AddSubjects(arguments[0]);
                    if (index >= 0)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        
        //Удалить предмет
        //Ok(200) - если предмет нормально удалился
        //BadRequst(400) - если неверный формат тела запроса
        //NotFound(404) - если не найден предмет с получеными данными
        [Route("")]
        [HttpDelete]
        public IHttpActionResult DeleteSubject([FromBody]string input)
        {
            if(input != null)
            {
                try
                {
                    var arguments = input.Split(' ');
                    string name = arguments[0];
                    int credits;
                    if(!Int32.TryParse(arguments[1], out credits))
                    {
                        throw new Exception();
                    }
                    var temp = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.AllSubjects;
                    var index = (from subject in temp
                                 where subject.Name == name
                                 select subject.Id).First();
                    if (SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Subjects.DeleteSubjects(index))
                    {
                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return NotFound();
        }
    }
}