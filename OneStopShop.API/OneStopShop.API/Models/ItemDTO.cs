using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Models
{
    public class ItemDTO
    {
        public int shoppinglistid { get; set; }

        public string shoppinglistitemdescription { get; set; }

        public string quantitydescription { get; set; }

        public bool itemchecked { get; set; }
    }
}
