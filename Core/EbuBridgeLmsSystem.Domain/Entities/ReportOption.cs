using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class ReportOption:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Report> reports { get; set; }
    }
}
