using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Models
{
    public class ShoppingListItemDTO
    {
        public int id { get; set; }

        public int ShoppingListID { get; set; }

        public string ShoppingListItemDescription { get; set; }

        public string QuantityDescription { get; set; }

        public bool ItemChecked { get; set; }
    }
}
