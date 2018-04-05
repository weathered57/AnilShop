using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnilShop.Entities.ViewsModel
{
    public class RegisterModel
    {
        [DisplayName("Kullanıcı Adı"),
          Required(ErrorMessage = "{0} alanını giriniz")

          StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir")]
        public string kullaniciAdi { get; set; }


        [DisplayName("Ad"),
             StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir"),
              Required(ErrorMessage = "{0} alanını giriniz")]
        public string ad { get; set; }

        [DisplayName("Soyad"),
           StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir"),
            Required(ErrorMessage = "{0} alanını giriniz")]
        public string soyAd { get; set; }

        [DisplayName("Şifre"),
             StringLength(20, ErrorMessage = "{0} en fazla {1} karakter olabilir"),
                  Required(ErrorMessage = "{0} alanını giriniz")
            DataType(DataType.Password)]
        public string sifre { get; set; }

        [DisplayName("Tekrar Şifre"),
             StringLength(20, ErrorMessage = "{0} en fazla {1} karakter olabilir"),
                  Required(ErrorMessage = "{0} alanını giriniz")
           DataType(DataType.Password),
            Compare("sifre")]
        public string tekrarSifre { get; set; }

        [DisplayName("E-mail"),
           StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir"),
                  Required(ErrorMessage = "{0} alanını giriniz")
           DataType(DataType.EmailAddress)]
        public string eMail { get; set; }
    }
}