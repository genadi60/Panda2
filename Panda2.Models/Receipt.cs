using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda2.Models
{
    public class Receipt
    {
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string RecipientId { get; set; }

        public PandaUser Recipient { get; set; }

        [Required]
        public string PackageId { get; set; }

        public Package Package { get; set; }
    }
}
