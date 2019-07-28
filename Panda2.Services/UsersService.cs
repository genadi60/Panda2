using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Panda2.Data;
using Panda2.Models;
using Panda2.Models.ViewModels;
using Panda2.Services.Contracts;

namespace Panda2.Services
{
    public class UsersService : IUsersService
    {
        private readonly Panda2DbContext _context;
        
        public UsersService(Panda2DbContext context)
        {
            _context = context;
        }

        public UserViewModel GetUserViewModelByIdWithPackages(string id)
        {
            var model = _context.Users
                .Where(u => u.Id.Equals(id))
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Username = u.UserName
                })
                .First();

            model.Packages = _context.Packages
                .Where(p => p.RecipientId.Equals(model.Id))
                .Include(p => p.Status)
                .Where(p => !p.Status.Name.Equals("Acquired"))
                .Select(p => new PackageViewModel
                {

                    Id = p.Id,
                    Description = p.Description,
                    Status = p.Status.Name
                    
                })
                .ToList();

            return model;
        }

        public AdministratorViewModel GetAdministratorViewModel()
        {
            var packages = _context.Packages
                .Include(p => p.Status)
                .Include(p => p.Recipient)
                .ToList();
            

            var model = new AdministratorViewModel
            {
                Pending = packages.Where(p => p.Status.Name.Equals("Pending")).Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight,
                    ShippingAddress = p.ShippingAddress,
                    Recipient = p.Recipient.UserName
                }).ToList(),
                Shipped = packages.Where(p => p.Status.Name.Equals("Shipped")).Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight,
                    ShippingAddress = p.ShippingAddress,
                    Recipient = p.Recipient.UserName
                }).ToList(),
                Delivered = packages.Where(p => p.Status.Name.Equals("Delivered") || p.Status.Name.Equals("Acquired")).Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight,
                    ShippingAddress = p.ShippingAddress,
                    Recipient = p.Recipient.UserName,
                    Status = p.Status.Name
                }).ToList()
            };

            return model;
        }

        public PandaUserRole GetRoleByUserId(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.Equals(id));
            if (user != null)
            {
                var roleId = user.RoleId;
                
                var role = _context.Roles.FirstOrDefault(r => r.Id.Equals(roleId));
                return role;
            }

            return null;
        }

        public CookieOptions ConfirmLogout()
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            return options;
        }
    }
}
