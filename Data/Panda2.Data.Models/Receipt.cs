using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Panda2.Data.Common.Models;

namespace Panda2.Data.Models
{
    public class Receipt : BaseDeletableModel<string>
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string RecipientId { get; set; }

        public virtual PandaUser Recipient { get; set; }

        [Required]
        public string PackageId { get; set; }

        public virtual Package Package { get; set; }
    }
}
