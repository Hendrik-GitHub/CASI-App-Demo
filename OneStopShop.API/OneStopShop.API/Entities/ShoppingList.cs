using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Entities
{
    public class ShoppingList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int shoppinglistid { get; set; }

        public string name { get; set; }

        public string description { get; set; }      

        public DateTime insertiondate { get; set; }

        public int userid { get; set; }

        [ForeignKey("userid")]
        public User User { get; set; }
    }
}
