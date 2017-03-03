using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Credits { get; set; }
    }

    public class Auditory
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }
}
