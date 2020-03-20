using Panda2.Data.Common.Repositories;
using Panda2.Data.Models;
using Panda2.Data.Models.InputModels;
using Panda2.Data.Models.ViewModels;
using Panda2.Services.Data.Contracts;
using Panda2.Services.Mapping;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Panda2.Services.Data
{
    public class PackagesService : IPackagesService
    {
        private readonly IDeletableEntityRepository<Package> _packageRepository;
        private readonly IDeletableEntityRepository<Status> _statusRepository;
        
        public PackagesService(IDeletableEntityRepository<Package> packageRepository, IDeletableEntityRepository<Status> statusRepository)
        {
            _packageRepository = packageRepository;
            _statusRepository = statusRepository;
        }

        public PackageViewModel GetPackage(string id)
        {
            var model = _packageRepository.All()
                .Where(p => p.Id.Equals(id))
                .To<PackageViewModel>()
                .SingleOrDefault();

            if (model != null)
            {
                if (model.StatusName.Equals("Pending"))
                {
                    model.EstimatedDeliveryDate = "N/A";
                }
                else if (model.StatusName.Equals("Delivered") || model.StatusName.Equals("Acquired"))
                {
                    model.EstimatedDeliveryDate = "Delivered";
                }
                else
                {
                    model.EstimatedDeliveryDate = DateTime.Parse(model.EstimatedDeliveryDate).ToString("dd/MM/yyyy", new DateTimeFormatInfo());
                }
            }

            return model;
        }
        
        public async Task<int> Create(PackageInputModel model)
        {
            var package = new Package
            {
                Description = model.Description,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress,
                RecipientId = model.RecipientId,
                CreatedOn = DateTime.UtcNow
            };

            return await UpdateDatabase(package, "Pending");
        }

        public async Task<int> Ship(string id)
        {
            var package = _packageRepository.All().SingleOrDefault(p => p.Id.Equals(id));
            if (package == null)
            {
                return 0;
            }

            Random random = new Random();
            var deliveryDuration = random.Next(20, 41);

            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(deliveryDuration);

            return await UpdateDatabase(package, "Shipped");
        }

        public async Task<int> Deliver(string id)
        {
            var package = _packageRepository.All().SingleOrDefault(p => p.Id.Equals(id));
            if (package == null)
            {
                return 0;
            }

            package.EstimatedDeliveryDate = DateTime.UtcNow;

            return await UpdateDatabase(package, "Delivered");
        }

        public async Task<int> Acquire(string id)
        {
            var package = _packageRepository.All().SingleOrDefault(p => p.Id.Equals(id));
            if (package == null)
            {
                return 0;
            }

            return await UpdateDatabase(package, "Acquire");
        }

        public async Task<int> UpdateDatabase(Package package, string statusName)
        {
            var status = _statusRepository.All().SingleOrDefault(s => s.Name.Equals(statusName));
            package.Status = status;


            _packageRepository.Update(package);

            return await _packageRepository.SaveChangesAsync();
        }
    }
}
