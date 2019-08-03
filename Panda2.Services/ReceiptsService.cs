using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Panda2.Data;
using Panda2.Models;
using Panda2.Models.ViewModels;
using Panda2.Services.Contracts;

namespace Panda2.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly Panda2DbContext _context;

        public ReceiptsService(Panda2DbContext context)
        {
            _context = context;
        }

        public UserViewModel GetUserViewModelByIdWithReceipts(string id)
        {
            var model = _context.Users
                .Include(u => u.Receipts)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Receipts = u.Receipts
                        .Select(r => new ReceiptViewModel
                        {
                            Id = r.Id,
                            Fee = r.Fee,
                            IssuedOn = r.IssuedOn.ToString("dd/MM/yyyy"),
                            Recipient = r.Recipient.UserName
                        })
                        .ToList()
                })
                .SingleOrDefault(u => u.Id.Equals(id));
            return model;
        }

        public bool Create(string id, string userId)
        {
            var package = _context.Packages.Include(p => p.Recipient).SingleOrDefault(p => p.Id.Equals(id) && p.RecipientId.Equals(userId));

            if (package == null)
            {
                return false;
            }

            var price = (decimal)package.Weight * 2.67M;

            var receipt = new Receipt
            {
                Fee = price,
                Recipient = package.Recipient,
                Package = package
            };

            var status = _context.Statuses.SingleOrDefault(s => s.Name.Equals("Acquired"));

            package.Status = status;

            _context.Receipts.Add(receipt);
            _context.SaveChanges();

            _context.Packages.Update(package);
            _context.SaveChanges();

            return true;
        }

        public ReceiptViewModel GetReceiptViewModel(string id)
        {
            var model = _context.Receipts
                .Include(r => r.Recipient)
                .Include(r => r.Package)
                .Select(r => new ReceiptViewModel
                {
                    Id = r.Id,
                    IssuedOn = r.IssuedOn.ToString("dd/MM/yyyy"),
                    DeliveryAddress = r.Package.ShippingAddress,
                    PackageWeight = r.Package.Weight,
                    PackageDescription = r.Package.Description,
                    Recipient = r.Recipient.UserName,
                    Fee = r.Fee
                })
                .SingleOrDefault(r => r.Id.Equals(id));

            return model;
        }
    }
}
