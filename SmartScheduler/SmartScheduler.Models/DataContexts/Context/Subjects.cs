using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface ISubjectsDbContext
    {
        IEnumerable<Subject> AllSubjects { get; }
        int AddSubjects(string name, int credits = 0);
        bool UpdateSubject(int id, string newName, int newCredits);
        bool DeleteSubjects(int id);
    }
    public class Subjects : ISubjectsDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Subjects(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Subject> AllSubjects
        {
            get
            {
                return Context.Subjects.ToList().Select(x=>x.Convert());
            }
        }

        public int AddSubjects(string name, int credits = 0)
        {
            var item = Context.Subjects.FirstOrDefault(x=>x.Name == name);
            if (item != null)
            {
                return item.SubjectId;
            }
            item = new DbSubject() { Name = name, Credits = credits };

            Context.Subjects.Add(item);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return item.SubjectId;
        }

        public bool DeleteSubjects(int id)
        {
            var item = Context.Subjects.FirstOrDefault(x => x.SubjectId == id);
            if (item == null) return false;

            Context.Subjects.Remove(item);
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

        public bool UpdateSubject(int id, string newName, int newCredits)
        {
            var item = Context.Subjects.FirstOrDefault(x => x.SubjectId == id);
            if (item == null) return false;

            item.Name = newName;
            item.Credits = newCredits;
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
