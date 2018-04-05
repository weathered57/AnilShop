using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("SepetUrun")]
    public class SepetUrun : Base
    {
        public virtual Urun urun { get; set; }

        public virtual Sepet sepet { get; set; }
    }
}
