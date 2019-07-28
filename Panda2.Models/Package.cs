using System;
using System.ComponentModel.DataAnnotations;

namespace Panda2.Models
{
    public class Package
    {
        public string Id { get; set; }

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
