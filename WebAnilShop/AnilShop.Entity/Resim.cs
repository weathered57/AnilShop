using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Resim")]
    public class Resim : Base
    {
        public string filenames { get; set; }

        public virtual Urun urun { get; set; }
    }
}
