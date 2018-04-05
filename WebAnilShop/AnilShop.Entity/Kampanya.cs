using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Kampanya")]
    public class Kampanya : Base
    {
        [Required]
        public string kampanyaAdi { get; set; }

        [Required]
        public decimal kampanyaYuzdesi { get; set; }

        public virtual List<Urun> urunler { get; set; }
    }
}
