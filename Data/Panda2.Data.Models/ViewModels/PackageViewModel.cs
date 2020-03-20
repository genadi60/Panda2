using Panda2.Services.Mapping;

namespace Panda2.Data.Models.ViewModels
{
    public class PackageViewModel : IMapFrom<Package>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public string EstimatedDeliveryDate { get; set; }

        public string RecipientId { get; set; }
        public string Recipient { get; set; }
    }
}
