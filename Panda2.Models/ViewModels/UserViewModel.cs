using System;
using System.Collections.Generic;

namespace Panda2.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Packages = new List<PackageViewModel>();
            this.Receipts = new List<ReceiptViewModel>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public ICollection<PackageViewModel> Packages { get; set; }

        public ICollection<ReceiptViewModel> Receipts { get; set; }
    }
}
