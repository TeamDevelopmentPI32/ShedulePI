using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Models.Models
{
    interface IModelId
    {
        int UserId { get; set; }
    }

    interface IModelNameId: IModelId
    {
        string Name { get; set; }
    }
}
