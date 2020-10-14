using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.DTOs
{
    public class OptionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OptionGroupId { get; set; }
        public string OptionGroupName { get; set; }
        public int Stock { get; set; }
    }
}
