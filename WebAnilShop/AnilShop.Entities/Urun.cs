using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{

    [Table("Urun")]
    public class Urun : Base
    {

        [Required]
        public string UrunAdi { get; set; }

        [Required]
        public string UrunAcıklamasi { get; set; }

        public decimal fiyat { get; set; }

        public decimal indirimlifiyat { get; set; }

        public string urunImage { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public DateTime DuzenlemeTarihi { get; set; }

        public virtual Kategori kategori { get; set; }

        public virtual List<Yorum> Yorumlar { get; set; }

        public virtual List<Sepet> Sepetler { get; set; }

        public virtual List<Resim> resimler { get; set; }

        public virtual Kampanya kampanya { get; set; }
    }
}
