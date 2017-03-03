using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.DataContexts.Context
{
    public interface IGroupsDbContext
    {
        IEnumerable<Group> AllGroups { get; }
        Group GetGrop(int id);
        int AddGroup(string name);
        bool DeleteGroup(int id);
        bool AddStudentInGroup(int studentId, int groupId);
        bool DeleteStrudentFromGroup(int studentId, int groupId);
        IEnumerable<Group> GetStudentGroups(int studentId);
    }
    public class Groups : IGroupsDbContext
    {
        private SmartSchedulerEntities Context { get; set; }

        public Groups(SmartSchedulerEntities dbContext)
        {
            Context = dbContext;
        }

        public IEnumerable<Group> AllGroups
        {
            get
            {
                return Context.Groups.Select(x=>x.Convert());
            }
        }

        public int AddGroup(string name)
        {
            var item = Context.Groups.FirstOrDefault(x=>x.Name == name);
            if (item != null)
            {
                return item.GroupId;
            }
            item = new DbGroup() { Name = name };

            Context.Groups.Add(item);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return -1;
            }
            return item.GroupId;
        }

        public bool DeleteGroup(int id)
        {
            var item = Context.Groups.FirstOrDefault(x => x.GroupId == id);
            if (item == null) return false;

            Context.Groups.Remove(item);
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

        public Group GetGrop(int id)
        {
            var group = Context.Groups.FirstOrDefault(x=> x.GroupId == id);

            if (group == null) return null;

            return group.Convert();
        }

        public bool AddStudentInGroup(int studentId, int groupId)
        {
            var student = Context.Students.FirstOrDefault(x=>x.StudentId == studentId);
            var group = Context.Groups.FirstOrDefault(x => x.GroupId == groupId);
            if (student == null || group == null) return false;

            var item = new DbStudentsInGroup()
            {
                Group = group,
                Student = student
            };
            Context.StudentsInGroups.Add(item);

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

        public bool DeleteStrudentFromGroup(int studentId, int groupId)
        {
            var student = Context.Students.FirstOrDefault(x => x.StudentId == studentId);
            var group = Context.Groups.FirstOrDefault(x => x.GroupId == groupId);
            if (student == null || group == null) return false;

            var item  = Context.StudentsInGroups.FirstOrDefault(x => x.Student == student && x.Group == group);
            if (item == null) return false;

            Context.StudentsInGroups.Remove(item);

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

        public IEnumerable<Group> GetStudentGroups(int studentId)
        {
            return Context.StudentsInGroups.Where(x => x.StudentId == studentId).Select(x=>x.Group.Convert());
        }
    }

}
