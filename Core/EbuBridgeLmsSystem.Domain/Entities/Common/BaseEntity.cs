using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities.Common
{
    public class BaseEntity
    {
        
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    
    }
}
