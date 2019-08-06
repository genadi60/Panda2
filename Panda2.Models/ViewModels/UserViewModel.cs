using System.Collections.Generic;
using System.Security.Cryptography;
using AutoMapper;
using Panda2.Services.Mapping;

namespace Panda2.Data.Models.ViewModels
{
    public class UserViewModel : IMapFrom<PandaUser>
    {
        public UserViewModel()
        {
            this.Packages = new List<PackageViewModel>();
            this.Receipts = new List<ReceiptViewModel>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public virtual ICollection<PackageViewModel> Packages { get; set; }

        public virtual ICollection<ReceiptViewModel> Receipts { get; set; }
    }
}
