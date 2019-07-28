using Panda2.Data;
using Panda2.Models;
using Panda2.Models.InputModels;
using Panda2.Models.ViewModels;
using Panda2.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Panda2.Services
{
    public class PackagesService : IPackagesService
    {
        private readonly Panda2DbContext _context;

        public PackagesService(Panda2DbContext context)
        {
            this._context = context;
        }


        public PackageViewModel GetPackage(string id)
        {
            var model = _context.Packages
                .Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight,
                    ShippingAddress = p.ShippingAddress,
                    Recipient = p.Recipient.UserName,
                    Status = p.Status.Name,
                    EstimatedDeliveryDate = p.EstimatedDeliveryDate == null ? "N/A" : ((DateTime)p.EstimatedDeliveryDate).ToString("dd/MM/yyyy")
                })
                .FirstOrDefault(p => p.Id.Equals(id));

            if (model != null && (model.Status.Equals("Delivered") || model.Status.Equals("Acquired")))
            {
                model.EstimatedDeliveryDate = "Delivered";
            }

            return model;
        }

        public int Create(PackageInputModel model)
        {
            var package = new Package
            {
                Description = model.Description,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress,
                RecipientId = model.RecipientId
            };

            var status = _context.Statuses.FirstOrDefault(s => s.Name.Equals("Pending"));
            package.Status = status;

            _context.Packages.Add(package);
             
            return SaveChangesAsync().Result;
        }

        public int Ship(string id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id.Equals(id));
            if (package == null)
            {
                return 0;
            }

            var status = _context.Statuses.FirstOrDefault(s => s.Name.Equals("Shipped"));
            package.Status = status;

            Random random = new Random();
            var deliveryDuration = random.Next(20, 41);

            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(deliveryDuration);

            _context.Packages.Update(package);
            //_context.SaveChanges();

            return SaveChangesAsync().Result;
        }

        public int Deliver(string id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id.Equals(id));
            if (package == null)
            {
                return 0;
            }

            var status = _context.Statuses.FirstOrDefault(s => s.Name.Equals("Delivered"));
            package.Status = status;

            package.EstimatedDeliveryDate = DateTime.UtcNow;

            _context.Packages.Update(package);
            //_context.SaveChanges();

            return SaveChangesAsync().Result;
        }

        public int Acquire(string id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id.Equals(id));
            if (package == null)
            {
                return 0;
            }

            var status = _context.Statuses.FirstOrDefault(s => s.Name.Equals("Acquired"));
            package.Status = status;
            

            _context.Packages.Update(package);
            //_context.SaveChanges();

            return SaveChangesAsync().Result;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await  _context.SaveChangesAsync();
        }
    }
}
