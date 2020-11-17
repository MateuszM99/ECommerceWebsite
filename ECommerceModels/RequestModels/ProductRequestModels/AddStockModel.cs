using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.RequestModels.ProductRequestModels
{
    public class AddStockModel
    {
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public int Stock { get; set; }
    }
}
