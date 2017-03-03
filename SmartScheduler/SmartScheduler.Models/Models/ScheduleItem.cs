using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.Models
{
    public class ScheduleItem
    {
        public int Id { get; set; }

        public int NumberLeson { get; set; }

        public Schedule Schedule { get; set; }

        public Teacher Teacher { get; set; }

        public Group Group { get; set; }

        public Auditory Auditory { get; set; }

        public Subject Subjcect { get; set; }
    }
}
