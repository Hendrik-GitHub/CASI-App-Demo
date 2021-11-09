using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Entities
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tokenid { get; set; }

        public int userid { get; set; }

        public string token { get; set; }

        public DateTime expires { get; set; }

        public DateTime created { get; set; }

        public string createdbyipaddress { get; set; }

        public DateTime? revoked { get; set; }

        public string revokedbyipaddress { get; set; }

        public string replacedbytoken { get; set; }
    }
}
