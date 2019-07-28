using System.Collections.Generic;

namespace Panda2.Models.ViewModels
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

        public ICollection<PackageViewModel> Pending { get; set; }

        public ICollection<PackageViewModel> Shipped { get; set; }

        public ICollection<PackageViewModel> Delivered { get; set; }
    }
}
