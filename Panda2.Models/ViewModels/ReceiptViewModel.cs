using System;
using System.Globalization;
using Panda2.Services.Mapping;

namespace Panda2.Data.Models.ViewModels
{
    public class ReceiptViewModel : IMapFrom<Receipt>
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
        public virtual PackageViewModel Package { get; set; }
    }
}
