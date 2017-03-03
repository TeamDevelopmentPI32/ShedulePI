using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.Models
{
    public class User : IModelId
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime RegistredDate { get; set; }

        public DateTime LastActivityDate { get; set; }
    }
}
