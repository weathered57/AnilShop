using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Kategori")]
    public class Kategori : Base
    {

        [Required]
        public string KategoriAdi { get; set; }

        public virtual List<Urun> Urunler { get; set; }
    }
}
