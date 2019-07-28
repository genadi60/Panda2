namespace Panda2.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public string IssuedOn { get; set; } 

        public string RecipientId { get; set; }
        public string Recipient { get; set; }

        public string DeliveryAddress{ get; set; }

        public double PackageWeight { get; set; }

        public string PackageDescription { get; set; }

        public string PackageId { get; set; }
        public PackageViewModel Package { get; set; }
    }
}
