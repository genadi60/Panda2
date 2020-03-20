using Panda2.Services.Mapping;

namespace Panda2.Data.Models.InputModels
{
    public class PackageInputModel : IMapTo<Package>
    {
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string RecipientId { get; set; }
    }
}
