using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Yorum")]
    public class Yorum : Base
    {

        [Required]
        public string aciklama { get; set; }

        public bool isAktif { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public DateTime DuzenlemeTarihi { get; set; }

        public virtual Musteri musteri { get; set; }

        public virtual Urun urun { get; set; }

        public Yorum()
        {
            EklenmeTarihi = DateTime.Now;

            DuzenlemeTarihi = DateTime.Now;
        }

    }
}
