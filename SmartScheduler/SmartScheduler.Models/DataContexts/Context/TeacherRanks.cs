using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface ITeacherRanksDbContext
    {
        IEnumerable<TeacherRank> Ranks { get; }
        int AddRank(string name);
        bool DeleteRank(int id);
    }
    public class TeacherRanks : ITeacherRanksDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public TeacherRanks(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<TeacherRank> Ranks
        {
            get
            {
                return Context.Ranks.ToList().Select(x=>x.Convert());
            }
        }

        public int AddRank(string name)
        {
            var item = Context.Ranks.FirstOrDefault(x=>x.RankName == name);
            if (item != null)
            {
                return item.RankId;
            }
            item = new DbRank() { RankName = name };

            Context.Ranks.Add(item);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return item.RankId;
        }

        public bool DeleteRank(int id)
        {
            var item = Context.Ranks.FirstOrDefault(x => x.RankId == id);
            if (item == null) return false;

            Context.Ranks.Remove(item);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return false;
            }
            return true;
        }
    }

}
