using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.Models
{
    public class Teacher:User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public int EntryYear { get; set; }
        public TeacherRank Rank { get; set; }
    }

    public class TeacherRank
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
