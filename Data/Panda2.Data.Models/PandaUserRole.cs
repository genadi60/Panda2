using System;
using Microsoft.AspNetCore.Identity;
using Panda2.Data.Common.Models;

namespace Panda2.Data.Models
{
    public class PandaUserRole : IdentityRole, IDeletableEntity
    {
        public PandaUserRole()
        {
            
        }

        public PandaUserRole(string name)
        {
            Name = name;
            NormalizedName = name.ToUpper();
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
