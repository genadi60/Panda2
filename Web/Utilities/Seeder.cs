using Panda2.Data;
using System.Collections.Generic;
using System.Linq;
using Panda2.Data.Models;

namespace Panda2.Utilities
{
    public static class Seeder
    {
        public static void Seed(Panda2DbContext context)
        {
            var roleNames = new List<string>()
            {
                "Administrator",
                "User",
                "Guest"
            };

            var contextRoles = context.Roles.Select(r => r.Name).ToList();

            if (roleNames.Count > contextRoles.Count)
            {
                
                var roles = new List<PandaUserRole>();

                foreach (var roleName in roleNames)
                {
                    if (!contextRoles.Contains(roleName))
                    {
                        var role = new PandaUserRole(roleName);
                        roles.Add(role);
                    }
                }

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
            

            var statusNames = new List<string>()
            {
                "Pending",
                "Shipped",
                "Delivered",
                "Acquired",
                "Test"
            };

            var contextStatuses = context.Statuses.Select(s => s.Name).ToList();

            if (statusNames.Count > contextStatuses.Count)
            {
                
                var statuses = new List<Status>();

                foreach (var statusName in statusNames)
                {
                    if (!contextStatuses.Contains(statusName))
                    {
                        var status = new Status(statusName);
                        statuses.Add(status);
                    }
                }

                context.Statuses.AddRange(statuses);
                context.SaveChanges();
            }
        }
    }
}