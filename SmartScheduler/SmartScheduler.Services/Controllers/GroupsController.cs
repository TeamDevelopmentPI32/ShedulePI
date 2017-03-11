using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartScheduler.Services.Controllers
{
    [RoutePrefix("api/groups")]
    public class GroupsController : ApiController
    {
        //Вернуть список всех груп
        [Route("")]
        [HttpGet]
        public List<Group> GetAllGroups()
        {
            return SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.AllGroups.ToList();
        }

        //Вернуть конкретную группу по названию
        [Route("{name: alpha}")]
        [HttpGet]
        public Group GetGroupByName(string name)
        {
            var temp = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.AllGroups;
            var indexes = (from item in temp
                          where item.Name == name
                          select item.Id).ToList();
            if(indexes.Count == 1)
            {
                return SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.GetGrop(indexes[0]);
            }
            return null;
        }

        //Вернуть конкретную группу по индексу
        [Route("{id: int}")]
        [HttpGet]
        public Group GetGroupById(int id)
        {
            return SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.GetGrop(id);
        }

        //Добавить группу 
        //Ok(200) - группа успешно добавлена
        //BadRequest(400) - неподходящий формат запроса
        [Route("{name: alpha}")]
        [HttpPost]
        public IHttpActionResult AddGroup(string name)
        {
            if(name != null)
            {
                int temp = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.AllGroups.Count();
                SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.AddGroup(name);
                if(SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.AllGroups.Count() != temp)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        //Удалить группу
        //Ok(200) - группа успешно удалена
        //BadRequest(400) - неверный формат запроса
        //NotFound(404) - группа с таким название отсутствует
        [Route("{name: alpha")]
        [HttpDelete]
        public IHttpActionResult DeleteGroup(string name)
        {
            if(name != null)
            {
                var groups = SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.AllGroups;
                var indexes = (from item in groups
                               where item.Name == name
                               select item.Id).ToList();
                if (indexes.Count == 1)
                {
                    if (SmartScheduler.Models.DataContexts.Context.SmartSchedulerContext.Instance.Groups.DeleteGroup(indexes[0]))
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            return BadRequest();
        }
    }
}