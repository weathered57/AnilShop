using AnilShop.BusinessLayer;
using AnilShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnilShop.Web;
using AnilShop.Entities.ViewsModel;
using AnilShop.BusinessLayer.Result;
using AnilShop.BusinessLayer.Messages;

namespace AnilShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private MusteriManager mm = new MusteriManager();
        private UrunManager um = new UrunManager();
        private KategoriManager km = new KategoriManager();
        private KampanyaManager kampanya = new KampanyaManager();
        private HakkimizdaManager hakkimizda = new HakkimizdaManager();
        private IletisimManager iletisim = new IletisimManager();
        BusinessLayerResult<Musteri> res = new BusinessLayerResult<Musteri>();
        // GET: Home
        public ActionResult Index()
        {
            var urunler = um.ListQueryable().Take(12).ToList();

            return View(urunler);
        }
        public ActionResult UrunlerSayfasi()
        {
            if (TempData["kampanyaliUrunler"] != null)
            {
                List<Urun> urunler = TempData["kampanyaliUrunler"] as List<Urun>;
                ViewBag.ad = "Kampanyalı Ürünler";
                return View(urunler);
            }
            if (TempData["yeniUrunler"] != null)
            {
                List<Urun> urunler = TempData["yeniUrunler"] as List<Urun>;
                ViewBag.ad = "Yeni Ürünler";
                return View(urunler);
            }
            else
            {
                ViewBag.ad = "Ürünler";
                return View(um.List());
            }

        }
        public ActionResult UrunDetayi(int id)
        {


            return View(um.Find(x => x.ID == id));
        }
        public ActionResult KategorilerSayfasi(int? id)
        {
            List<Urun> urunler = um.List();

            if (id == null)
            {
                return View(urunler);
            }
            if (id == 0)
            {
                return View(urunler);
            }
            else
            {
                Kategori kategori = km.Find(x => x.ID == id);

                return View(kategori.Urunler);
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(loginModel model)
        {

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Musteri> res = mm.LoginMusteri(model);

                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        return RedirectToAction("AktifDegil");
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }
                else
                {
                    Session["login"] = res.result;

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public ActionResult Register()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Musteri> res = mm.RegisterMusteri(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }
                else
                {
                    return RedirectToAction("RegisterOk");
                }

            }

            return View(model);
        }
        public ActionResult RegisterOk()
        {
            return View();
        }
        public ActionResult AktifDegil()
        {
            return View();
        }
        public ActionResult Cikis()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult KampanyaliUrunler()
        {

            List<Urun> urunler = um.List(x => x.kampanya.kampanyaAdi != "bos");
            TempData["kampanyaliUrunler"] = urunler;

            return RedirectToAction("UrunlerSayfasi");
        }

        public ActionResult YeniGelenler()
        {
            List<Urun> urunler = um.ListQueryable().OrderByDescending(x => x.EklenmeTarihi).Take(20).ToList();
            TempData["yeniUrunler"] = urunler;

            return RedirectToAction("UrunlerSayfasi");
        }

        public ActionResult KullaniciBulunamadi()
        {
            return View();
        }

        public ActionResult ProfilGoster()
        {
            Musteri musteri = Session["login"] as Musteri;

            res = mm.MusteriGetirId(musteri.ID);
            if (musteri == null)
            {
                return RedirectToAction("KullaniciBulunamadi");
            }

            if (res.Errors.Count > 0)
            {
                if ((res.Errors.Find(x => x.Code == ErrorMessageCode.UserNotFound) != null))
                {
                    return RedirectToAction("KullaniciBulunamadi");

                }
            }
            return View(res.result);
        }

        public ActionResult ProfilDuzenle(int? id)
        {
            res = mm.MusteriGetirId(id);

            if (res.Errors.Count > 0)
            {
                if ((res.Errors.Find(x => x.Code == ErrorMessageCode.UserNotFound) != null))
                {
                    return RedirectToAction("KullaniciBulunamadi");

                }
            }

            return View(res.result);
        }
        [HttpPost]
        public ActionResult ProfilDuzenle(Musteri model, HttpPostedFileBase ProfilImage)
        {

            if (ModelState.IsValid)
            {
                if (ProfilImage != null &&
                    (ProfilImage.ContentType == "image/jpeg" ||
                    ProfilImage.ContentType == "image/jpg" ||
                    ProfilImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.ID}.{ProfilImage.ContentType.Split('/')[1]}";

                    ProfilImage.SaveAs(Server.MapPath($"~/img/{filename}"));
                    model.musteriImagae = filename;
                }
                res = mm.MusteriDuzenle(model);

                if (res.Errors.Count > 0)
                {
                    if ((res.Errors.Find(x => x.Code == ErrorMessageCode.UsernameAlreadyExits) == null))
                    {
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(model);
                    }
                    if ((res.Errors.Find(x => x.Code == ErrorMessageCode.EmailAlreadyExits) == null))
                    {
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(model);
                    }
                    return RedirectToAction("KullaniciBulunamadi");
                }

                Session["login"] = res.result;

                return RedirectToAction("ProfilGoster");
            }
            return View(model);
        }

        public ActionResult DeleteProfile()
        {
            Musteri musteri = Session["login"] as Musteri;

            res = mm.RemoveUserById(musteri.ID);

            if (res.Errors.Count > 0)
            {

                return View("KullaniciBulunamadi");
            }

            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult UserActivate(Guid id)
        {
            res = mm.ActivateUser(id);

            if (res.Errors.Count > 0)
            {

                return View("Hata");
            }





            return View("OkActivate");
        }

        public ActionResult Hata()
        {
            return View();
        }
        public ActionResult OkActivate()
        {
            return View();
        }
        public ActionResult Hakkimizda()
        {
            Hakkimizda h = hakkimizda.Find(x => x.ID == 1);
            return View(h);
        }
        public ActionResult Iletisim()
        {
            Iletisim i = iletisim.Find(x => x.ID == 1);
            return View(i);
        }

    }
}