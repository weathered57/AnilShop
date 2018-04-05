using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Hakkimizda")]
    public class Hakkimizda : Base
    {
        public string Baslik { get; set; }

        public string aciklama { get; set; }

        public string hakkimzdaImage { get; set; }


    }
}
