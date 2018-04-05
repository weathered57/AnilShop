using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Adresler")]
    public class Adresler : Base
    {
        [Required]
        public string adresTanim { get; set; }

        public string sehir { get; set; }

        public string telefon { get; set; }

        public virtual Musteri musteri { get; set; }
    }
}
