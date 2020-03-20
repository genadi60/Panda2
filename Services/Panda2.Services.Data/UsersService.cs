using Microsoft.AspNetCore.Http;
using Panda2.Data.Common.Repositories;
using Panda2.Data.Models;
using Panda2.Data.Models.ViewModels;
using Panda2.Services.Data.Contracts;
using Panda2.Services.Mapping;
using System;
using System.Linq;

namespace Panda2.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<PandaUser> _userRepository;
        private readonly IDeletableEntityRepository<Package> _packageRepository;
        private readonly IDeletableEntityRepository<PandaUserRole> _roleRepository;

        public UsersService(IDeletableEntityRepository<PandaUser> userRepository, IDeletableEntityRepository<Package> packageRepository, IDeletableEntityRepository<PandaUserRole> roleRepository)
        {
            _userRepository = userRepository;
            _packageRepository = packageRepository;
            _roleRepository = roleRepository;
        }

        public UserViewModel GetUserViewModelByIdWithPackages(string id)
        {
            var model = _userRepository.All()
                .Where(u => u.Id.Equals(id))
                .To<UserViewModel>()
                .SingleOrDefault();

            if (model != null)
            {
                model.Packages = _packageRepository.All()
                    .Where(p => p.RecipientId.Equals(model.Id) && !p.Status.Name.Equals("Acquired"))
                    .To<PackageViewModel>()
                    .ToList();
            }

            return model;
        }

        public AdministratorViewModel GetAdministratorViewModel()
        {
            var packages = _packageRepository.All();
            
            var model = new AdministratorViewModel
            {
                Pending = packages
                    .Where(p => p.Status.Name.Equals("Pending"))
                    .To<PackageViewModel>()
                    .ToList(),
                Shipped = packages
                    .Where(p => p.Status.Name.Equals("Shipped"))
                    .To<PackageViewModel>()
                    .ToList(),
                Delivered = packages
                    .Where(p => p.Status.Name.Equals("Delivered") || p.Status.Name.Equals("Acquired"))
                    .To<PackageViewModel>()
                    .ToList()
            };

            return model;
        }

        public PandaUserRole GetRoleByUserId(string id)
        {
            var user = _userRepository.All().SingleOrDefault(u => u.Id.Equals(id));

            if (user == null)
            {
                return null;
            }

            return _roleRepository.All().SingleOrDefault(r => r.Id.Equals(user.RoleId));
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
