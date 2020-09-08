using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Models
{
    public class OptionGroup
    {
        [Key]
        public int OptionGroupId { get; set; }
        public string OptionGroupName { get; set; }
        public IList<Option> Options { get; set; }
    }
}
