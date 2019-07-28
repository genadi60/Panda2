using Microsoft.AspNetCore.Identity;

namespace Panda2.Models
{
    public class PandaUserRole : IdentityRole
    {
        public PandaUserRole()
        {
            
        }

        public PandaUserRole(string name)
        {
            Name = name;
            NormalizedName = name.ToUpper();
        }
    }
}
