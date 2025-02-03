using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class Address:BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; }
        public User  User { get; set; }

    }
}
