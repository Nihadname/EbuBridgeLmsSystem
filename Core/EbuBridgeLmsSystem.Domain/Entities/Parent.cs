using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class Parent:BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Student> Students { get; set; }  
    }
}
