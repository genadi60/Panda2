using System;
using System.ComponentModel.DataAnnotations;
using Panda2.Data.Common.Models;
using Panda2.Data.Models.InputModels;
using Panda2.Services.Mapping;

namespace Panda2.Data.Models
{
    public class Package : BaseDeletableModel<string>, IMapFrom<PackageInputModel>
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public PandaUser Recipient { get; set; }
    }
}
