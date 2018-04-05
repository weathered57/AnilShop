using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Siparis")]
    public class Siparis : Base
    {

        public string Fatura { get; set; }

        public virtual Musteri musteri { get; set; }

        public virtual SiparisDetay siparisDetay { get; set; }

    }
}
