using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartScheduler.Services.Controllers
{
    [RoutePrefix("api/teacherRank")]
    public class TeacherRankController : ApiController
    {
        //Вернуть список всех teacherRank
        [HttpGet]
        public List<TeacherRank> GetAllTeacherRanks()
        {
            return Models.DataContexts.Context.SmartSchedulerContext.Instance.TeacherRanks.Ranks.ToList();
        }

        //Вернуть teacherRank по id
        [HttpGet]
        public TeacherRank GetTeacherRankById([FromBody]int id)
        {
            if (id < 0)
            {
                var ranks = Models.DataContexts.Context.SmartSchedulerContext.Instance.TeacherRanks.Ranks;
                var indexes = (from item in ranks
                               where item.Id == id
                               select item).ToList();
                return indexes[0];
            }
            return null;
        }

        //Добавить teacherRank
        //Ok(200) - teacherRank успешно добавлен
        //BadRequest(400) - number < 0
        [HttpPost]
        public IHttpActionResult AddTeacherRank([FromBody]string name)
        {
            try
            {
                if (name == "")
                {
                    var ranks = Models.DataContexts.Context.SmartSchedulerContext.Instance.TeacherRanks.Ranks;
                    var indexes = (from item in ranks
                                   where item.Name == name
                                   select item).ToList();
                    if (indexes.Count == 0)
                    {
                        Models.DataContexts.Context.SmartSchedulerContext.Instance.TeacherRanks.AddRank(name);
                        return Ok();
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

        //Удалить teacherRank
        //Ok(200) - teacherRank успешно удален
        //BadRequest(400)
        //NotFound(404) - не найден teacherRank с таким номером
        [HttpDelete]
        public IHttpActionResult DeleteAuditory([FromBody]TeacherRank rank)
        {
            try
            {
                if (rank.Id < 0 || rank.Name != "")
                {
                    if(Models.DataContexts.Context.SmartSchedulerContext.Instance.TeacherRanks.DeleteRank(rank.Id))
                    {
                        return Ok();
                    }
                    return NotFound();
                }
                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
}

