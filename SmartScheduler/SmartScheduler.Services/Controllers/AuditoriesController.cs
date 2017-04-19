using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartScheduler.Services.Controllers
{
    [RoutePrefix("api/auditories")]
    public class AuditoriesController : ApiController
    {
        //Вернуть список всех аудиторий
        [HttpGet]
        public List<Auditory> GetAllAuditories()
        {
            return Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.AllAuditories.ToList();
        }

        //Вернуть аудиторию по номеру
        [HttpGet]
        public Auditory GetAuditoryByNumber([FromBody]int number)
        {
            if(number < 0)
            {
                var auditories = Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.AllAuditories;
                var indexes = (from item in auditories
                               where item.Number == number
                               select item).ToList();
                return indexes[0];
            }
            return null;
        }

        //Добавить аудиторию
        //Ok(200) - аудитория успешно добавлена
        //BadRequest(400) - number < 0 или аудитория с таким названием уже есть
        [HttpPost]
        public IHttpActionResult AddAuditory([FromBody]int number)
        {
            if(number < 0)
            {
                var auditories = Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.AllAuditories;
                var indexes = (from item in auditories
                               where item.Number == number
                               select item).ToList();
                if(indexes.Count == 0)
                {
                    Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.AddAuditory(number);
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        //Обновить аудиторию
        //Ok(200) - аудитория успешно добавлена
        //BadRequest(400) - number < 0
        //NotFound(404) - не найдена аудитория с таким номером
        //Conflict(409) - нельзя изменить номер адитории потому что такая уже есть
        [HttpPost]
        public IHttpActionResult UpdateAuditory([FromBody]Auditory auditory)
        {
            try
            {
                if (auditory.Id < 0 || auditory.Number < 0)
                {
                    var auditories = Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.AllAuditories;
                    var indexes = (from item in auditories
                                   where item.Id == auditory.Id
                                   select item).ToList();
                    if (indexes.Count == 0)
                    {
                        return NotFound();
                    }
                    if (indexes.Count == 1)
                    {
                        indexes = (from item in auditories
                                   where item.Number == auditory.Number
                                   select item).ToList();
                        if (indexes.Count == 0)
                        {
                            Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.UpdateAuditory(auditory.Id, auditory.Number);
                            return Ok();
                        }
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        //Удалить аудиторию
        //Ok(200) - аудитория успешно удалена
        //BadRequest(400) - number < 0
        //NotFound(404) - не найдена аудитория с таким номером
        [HttpDelete]
        public IHttpActionResult DeleteAuditory([FromBody]Auditory auditory)
        {
            try
            {
                if (auditory.Id < 0 || auditory.Number < 0)
                {
                    if(Models.DataContexts.Context.SmartSchedulerContext.Instance.Auditories.DeleteAuditory(auditory.Id))
                    {
                        return Ok();
                    }
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
