using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnilShop.Entities;
using AnilShop.BusinessLayer;

namespace AnilShop.Web.Areas.Admin.Controllers
{
    public class UrunController : Controller
    {
        private UrunManager u = new UrunManager();
        private KategoriManager k = new KategoriManager();
        private KampanyaManager kam = new KampanyaManager();

        List<SelectListItem> kategorilerList = new List<SelectListItem>();
        List<SelectListItem> kampanyaList = new List<SelectListItem>();

        // GET: Admin/Urun
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                Kategori kat = k.Find(x => x.ID == id);
                return View("Index", kat.Urunler);
            }
            return View(u.List());
        }

        // GET: Admin/Urun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = u.Find(x => x.ID == id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Admin/Urun/Create
        public ActionResult Create()
        {

            List<Kategori> kategori = k.List();
            List<Kampanya> kampanyalar = kam.List();

            foreach (var item in kategori)
            {
                SelectListItem sec = new SelectListItem();
                sec.Text = item.KategoriAdi;
                sec.Value = item.ID.ToString();

                kategorilerList.Add(sec);
            }
            foreach (var item in kampanyalar)
            {
                SelectListItem sec = new SelectListItem();
                sec.Text = item.kampanyaAdi;
                sec.Value = item.ID.ToString();

                kampanyaList.Add(sec);
            }

            TempData["kategoriler"] = kategorilerList as List<SelectListItem>;
            ViewBag.kategoriler = kategorilerList as List<SelectListItem>;
            TempData["kampanyalar"] = kampanyaList as List<SelectListItem>;
            ViewBag.kampanyalar = kampanyaList as List<SelectListItem>;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urun urun, HttpPostedFileBase ProfilImage)
        {

            ModelState.Remove("Kategori.KategoriAdi");
            ModelState.Remove("Kampanya.kampanyaAdi");

            if (ModelState.IsValid)
            {
                if (ProfilImage != null &&
                  (ProfilImage.ContentType == "image/jpeg" ||
                  ProfilImage.ContentType == "image/jpg" ||
                  ProfilImage.ContentType == "image/png"))
                {
                    string filename = $"urun_{urun.ID}.{ProfilImage.ContentType.Split('/')[1]}";

                    ProfilImage.SaveAs(Server.MapPath($"~/img/{filename}"));
                    urun.urunImage = filename;
                }

                Kategori kat = k.Find(x => x.ID == urun.kategori.ID);
                Kampanya kp = kam.Find(x => x.ID == urun.kampanya.ID);
                urun.EklenmeTarihi = DateTime.Now;
                urun.DuzenlemeTarihi = DateTime.Now;
                urun.kategori = kat;
                urun.kampanya = kp;
                u.Insert(urun);
                return RedirectToAction("Index");
            }
            ViewBag.kategoriler = TempData["kategoriler"] as List<SelectListItem>;
            ViewBag.kampanyalar = TempData["kampanyalar"] as List<SelectListItem>;
            return View(urun);


        }

        // GET: Admin/Urun/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Kampanya> kampanyalar = kam.List();
            List<Kategori> kategori = k.List();

            foreach (var item in kategori)
            {
                SelectListItem sec = new SelectListItem();
                sec.Text = item.KategoriAdi;
                sec.Value = item.ID.ToString();

                kategorilerList.Add(sec);
            }
            foreach (var item in kampanyalar)
            {
                SelectListItem sec = new SelectListItem();
                sec.Text = item.kampanyaAdi;
                sec.Value = item.ID.ToString();

                kampanyaList.Add(sec);
            }

            TempData["kategoriler"] = kategorilerList as List<SelectListItem>;
            ViewBag.kategoriler = kategorilerList as List<SelectListItem>;
            TempData["kampanyalar"] = kampanyaList as List<SelectListItem>;
            ViewBag.kampanyalar = kampanyaList as List<SelectListItem>;
            Urun urun = u.Find(x => x.ID == id);


            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Urun urun, HttpPostedFileBase ProfilImage)
        {

            ModelState.Remove("Kategori.KategoriAdi");
            ModelState.Remove("Kampanya.kampanyaAdi");
            if (ModelState.IsValid)
            {
                if (ProfilImage != null &&
                  (ProfilImage.ContentType == "image/jpeg" ||
                  ProfilImage.ContentType == "image/jpg" ||
                  ProfilImage.ContentType == "image/png"))
                {
                    string filename = $"urun_{urun.ID}.{ProfilImage.ContentType.Split('/')[1]}";

                    ProfilImage.SaveAs(Server.MapPath($"~/img/{filename}"));
                    urun.urunImage = filename;
                }

                Kategori kat = k.Find(x => x.ID == urun.kategori.ID);
                Kampanya kp = kam.Find(x => x.ID == urun.kampanya.ID);


                Urun dbUrun = u.Find(x => x.ID == urun.ID);
                dbUrun.fiyat = urun.fiyat;
                dbUrun.DuzenlemeTarihi = DateTime.Now;
                dbUrun.UrunAdi = urun.UrunAdi;
                dbUrun.UrunAcıklamasi = urun.UrunAcıklamasi;
                dbUrun.kategori = kat;
                dbUrun.kampanya = kp;
                dbUrun.urunImage = urun.urunImage;

                u.Update(urun);
            }

            ViewBag.kategoriler = TempData["kategoriler"] as List<SelectListItem>;
            ViewBag.kampanyalar = TempData["kampanyalar"] as List<SelectListItem>;

            return RedirectToAction("Index");
        }

        // GET: Admin/Urun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = u.Find(x => x.ID == id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Admin/Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = u.Find(x => x.ID == id);
            u.Delete(urun);

            return RedirectToAction("Index");
        }
    }
}
