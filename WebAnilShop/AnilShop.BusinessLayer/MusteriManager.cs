using AnilShop.BusinessLayer.Abstract;
using AnilShop.BusinessLayer.Messages;
using AnilShop.BusinessLayer.Result;
using AnilShop.Common.Helpers;
using AnilShop.DataAccessLAyer.EntityFramework;
using AnilShop.Entities;

using AnilShop.Entities.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.BusinessLayer
{
    public class MusteriManager : ManagerBase<Musteri>
    {
        private BusinessLayerResult<Musteri> layerResults = new BusinessLayerResult<Musteri>();


        public BusinessLayerResult<Musteri> RegisterMusteri(RegisterModel model)
        {
            Musteri musteri = Find(x => x.KullaniciAdi == model.kullaniciAdi || x.Email == model.eMail);

            if (musteri != null)
            {
                if (musteri.KullaniciAdi == model.kullaniciAdi)
                {
                    layerResults.AddError(ErrorMessageCode.UsernameAlreadyExits, "Kullanıcı Adı Kullanılıyor");
                }
                if (musteri.Email == model.eMail)
                {
                    layerResults.AddError(ErrorMessageCode.EmailAlreadyExits, "E-mail Kullanılıyor");
                }
            }
            else
            {
                int sonuc = Insert(new Musteri()
                {
                    KullaniciAdi = model.kullaniciAdi,
                    Email = model.eMail,
                    Ad = model.ad,
                    Soyad = model.soyAd,
                    Sifre = model.sifre,
                    musteriImagae = "profil.png",
                    activateGuid = Guid.NewGuid(),
                    isAdmin = false,
                    isAktif = false
                });

                if (sonuc > 0)
                {
                    layerResults.result = Find(x => x.Email == model.eMail && x.KullaniciAdi == model.kullaniciAdi);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResults.result.activateGuid}";
                    string body = $"Merhaba {layerResults.result.KullaniciAdi};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";

                    MailHelper.SendMail(body, layerResults.result.Email, "Anil Shop Hesap Aktifleştirme");
                }
            }
            return layerResults;
        }
        public BusinessLayerResult<Musteri> ActivateUser(Guid activateId)
        {

            layerResults.result = Find(x => x.activateGuid == activateId);

            if (layerResults.result != null)
            {
                if (layerResults.result.isAktif)
                {
                    layerResults.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return layerResults;
                }

                layerResults.result.isAktif = true;
                Update(layerResults.result);
            }
            else
            {
                layerResults.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return layerResults;
        }

        public BusinessLayerResult<Musteri> LoginMusteri(loginModel model)
        {
            Musteri musteri = Find(x => x.KullaniciAdi == model.kullaniciAdi && x.Sifre == model.sifre);

            if (musteri != null)
            {
                if (musteri.isAktif == true)
                {
                    layerResults.result = musteri;
                }
                else
                {
                    layerResults.AddError(ErrorMessageCode.UserIsNotActive, "Henüz Kullanıcı,Admin Tarafından Aktif Edilmemiştir");
                }

            }
            else
            {
                layerResults.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı Adı ve ya Şifre Uyuşmuyor");
            }

            return layerResults;
        }

        public BusinessLayerResult<Musteri> MusteriGetirId(int? id)
        {
            layerResults.result = Find(x => x.ID == id);

            if (layerResults.result == null)
            {
                layerResults.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");
            }
            return layerResults;
        }

        public BusinessLayerResult<Musteri> MusteriDuzenle(Musteri data)
        {
            Musteri db_user = Find(x => x.ID != data.ID && (x.KullaniciAdi == data.KullaniciAdi || x.Email == data.Email));

            if (db_user != null && db_user.ID != data.ID)
            {
                if (db_user.KullaniciAdi == data.KullaniciAdi)
                {
                    layerResults.AddError(ErrorMessageCode.UsernameAlreadyExits, "Kullanıcı Adı Kayıtlı");
                }
                if (db_user.Email == data.Email)
                {
                    layerResults.AddError(ErrorMessageCode.UsernameAlreadyExits, "Email Kayıtlı");
                }
                return layerResults;
            }
            layerResults.result = Find(x => x.ID == data.ID);
            layerResults.result.Email = data.Email;
            layerResults.result.Ad = data.Ad;
            layerResults.result.Soyad = data.Soyad;
            layerResults.result.Sifre = data.Sifre;
            layerResults.result.KullaniciAdi = data.KullaniciAdi;

            if (string.IsNullOrEmpty(data.musteriImagae) == false)
            {
                layerResults.result.musteriImagae = data.musteriImagae;
            }

            if (Update(layerResults.result) == 0)
            {
                layerResults.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return layerResults;
        }

        public BusinessLayerResult<Musteri> RemoveUserById(int id)
        {

            Musteri user = Find(x => x.ID == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    layerResults.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return layerResults;
                }
            }
            else
            {
                layerResults.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }

            return layerResults;
        }

    }


}
