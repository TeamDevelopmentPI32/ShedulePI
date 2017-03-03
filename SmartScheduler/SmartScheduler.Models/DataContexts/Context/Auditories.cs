using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface IAuditoriesDbContext
    {
        IEnumerable<Auditory> AllAuditories { get; }
        int AddAuditory(int number);
        bool DeleteAuditory(int id);
    }
    public class Auditories : IAuditoriesDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Auditories(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Auditory> AllAuditories
        {
            get
            {
                return Context.Auditories.Select(x=>x.Convert());
            }
        }

        public int AddAuditory(int number)
        {
            var item = Context.Auditories.FirstOrDefault(x=>x.Number == number);
            if (item != null)
            {
                return item.AuditoryId;
            }
            item = new DbAuditory() { Number = number };

            Context.Auditories.Add(item);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return item.AuditoryId;
        }

        public bool DeleteAuditory(int id)
        {
            var item = Context.Auditories.FirstOrDefault(x => x.AuditoryId == id);
            if (item == null) return false;

            Context.Auditories.Remove(item);
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
