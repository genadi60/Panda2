using System.Collections.Generic;
using AutoMapper;
using Panda2.Services.Mapping;

namespace Panda2.Data.Models.ViewModels
{
    public class AdministratorViewModel
    {
        public AdministratorViewModel()
        {
            this.Pending = new List<PackageViewModel>();
            this.Shipped = new List<PackageViewModel>();
            this.Delivered = new List<PackageViewModel>();
        }

        public string Name { get; set; }

        public virtual ICollection<PackageViewModel> Pending { get; set; }

        public virtual ICollection<PackageViewModel> Shipped { get; set; }

        public virtual ICollection<PackageViewModel> Delivered { get; set; }
    }
}
