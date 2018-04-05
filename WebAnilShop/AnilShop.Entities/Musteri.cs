using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Musteri")]
    public class Musteri : Base
    {

        [Required]
        public string KullaniciAdi { get; set; }

        [Required, DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required]
        public string Ad { get; set; }

        [Required]
        public string Soyad { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public bool isAdmin { get; set; }

        public bool isAktif { get; set; }

        public Guid activateGuid { get; set; }

        public string musteriImagae { get; set; }

        public DateTime EklenmeTarihi { get; set; }


        public virtual List<Yorum> yorumlar { get; set; }

        public virtual List<Siparis> siparisler { get; set; }

        public virtual List<Adresler> adresler { get; set; }

        public virtual Sepet sepet { get; set; }

        public Musteri()
        {
            EklenmeTarihi = DateTime.Now;
        }


    }
}
