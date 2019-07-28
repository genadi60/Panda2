using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Panda2.Models
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
        public PandaUserRole Role { get; set; }

        public ICollection<Package> Packages { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}
