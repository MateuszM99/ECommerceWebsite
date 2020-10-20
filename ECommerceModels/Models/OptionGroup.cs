using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Models
{
    public class OptionGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Option> Options { get; set; }
    }
}
