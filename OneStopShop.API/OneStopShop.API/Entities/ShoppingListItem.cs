using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Entities
{
    public class ShoppingListItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string shoppinglistitemdescription { get; set; }

        public int shoppinglistid { get; set; }

        public string quantitydescription { get; set; }

        public DateTime insertiondate { get; set; }

        public bool itemchecked { get; set; }

        [ForeignKey("shoppinglistid")]
        public ShoppingList ShoppingList { get; set; }
    }
}
