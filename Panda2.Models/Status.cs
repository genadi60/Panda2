using System.ComponentModel.DataAnnotations;

namespace Panda2.Models
{
    public class Status
    {
        public Status()
        {
            
        }

        public Status(string name)
        {
            this.Name = name;
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
