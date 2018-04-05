using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("SiparisDetay")]
    public class SiparisDetay : Base
    {
        public DateTime tarih { get; set; }

        public string durum { get; set; }

        public decimal tutar { get; set; }

        public virtual List<Siparis> siparis { get; set; }
    }
}
