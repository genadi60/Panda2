using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Panda2.Data.Models;

namespace Panda2.Data
{
    public class PandaUserRoleStore : RoleStore<
        PandaUserRole,
        Panda2DbContext,
        string,
        IdentityUserRole<string>,
        IdentityRoleClaim<string>>
    {
        public PandaUserRoleStore(Panda2DbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(PandaUserRole role, Claim claim) =>
            new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
