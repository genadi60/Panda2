using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Panda2.Data.Models
{
    public class PandaUser : IdentityUser
    {
        public PandaUser() 
        {
            this.Packages = new List<Package>();
            this.Receipts = new List<Receipt>();
        }


        [Required]
        public string RoleId { get; set; }
        public virtual PandaUserRole Role { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
