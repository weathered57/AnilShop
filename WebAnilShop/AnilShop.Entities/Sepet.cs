using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Sepet")]
    public class Sepet : Base
    {

        public decimal toplamTutar { get; set; }

        public virtual List<Musteri> musteriler { get; set; }

        public virtual List<Urun> urunler { get; set; }
    }
}
