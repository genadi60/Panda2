using Panda2.Data.Common.Models;

namespace Panda2.Data.Models
{
    public class Setting : BaseDeletableModel<string>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
