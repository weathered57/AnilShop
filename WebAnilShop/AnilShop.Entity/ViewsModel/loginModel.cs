using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnilShop.Entities.ViewsModel
{
    public class loginModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanını giriniz")
            StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir")]
        public string kullaniciAdi { get; set; }

        [DisplayName("Şifre"),
             StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir"),
                  Required(ErrorMessage = "{0} alanını giriniz")
            DataType(DataType.Password)]
        public string sifre { get; set; }


    }
}