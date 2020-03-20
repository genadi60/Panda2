using System.ComponentModel.DataAnnotations;
using Panda2.Data.Common.Models;

namespace Panda2.Data.Models
{
    public class Status : BaseDeletableModel<int>
    {
        public Status()
        {
            
        }

        public Status(string name)
        {
            this.Name = name;
        }
        [Required]
        public string Name { get; set; }
    }
}
