using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Entities
{
    public class ItemQuantityType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int quantitytypeid { get; set; }

        public string description { get; set; }

        public DateTime insertiondate { get; set; }
    }
}
