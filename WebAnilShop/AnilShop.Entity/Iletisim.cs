using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.Entities
{
    [Table("Iletisim")]
    public class Iletisim : Base
    {

        public string telNo { get; set; }

        public string eMail { get; set; }

        public string adres { get; set; }
    }
}
