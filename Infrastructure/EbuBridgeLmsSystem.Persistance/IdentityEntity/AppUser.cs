using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.IdentityEntity
{
    public class AppUser:IdentityUser
    {
        public User DomainUser { get; set; }

    }
}
