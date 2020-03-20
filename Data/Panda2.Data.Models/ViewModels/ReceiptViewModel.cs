using System;
using System.Globalization;
using AutoMapper;
using Panda2.Services.Mapping;

namespace Panda2.Data.Models.ViewModels
{
    public class ReceiptViewModel : IMapFrom<Receipt>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Receipt, ReceiptViewModel>()
                .ForMember(vm => vm.IssuedOn,
                    opt => opt.MapFrom(s => s.IssuedOn.ToString("dd/MM/yyyy", new DateTimeFormatInfo())));
        }
    }
}
