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
    public class YorumController : Controller
    {
        private YorumManager ym = new YorumManager();
        private MusteriManager mm = new MusteriManager();
        private UrunManager um = new UrunManager();
        // GET: Admin/Yorum
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                Urun urun = um.Find(x => x.ID == id);

                return View(urun.Yorumlar);
            }
            return View(ym.List());
        }

        // GET: Admin/Yorum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = ym.Find(x => x.ID == id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: Admin/Yorum/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                ym.Insert(yorum);
                return RedirectToAction("Index");
            }

            return View(yorum);
        }

        // GET: Admin/Yorum/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = ym.Find(x => x.ID == id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Yorum yorum)
        {
            Musteri musteri = mm.Find(x => x.ID == yorum.musteri.ID);
            Urun urun = um.Find(x => x.ID == yorum.urun.ID);

            ModelState.Remove("musteri.KullaniciAdi");
            ModelState.Remove("musteri.Sifre");
            ModelState.Remove("musteri.Ad");
            ModelState.Remove("musteri.Soyad");
            ModelState.Remove("musteri.Email");
            ModelState.Remove("urun.UrunAdi");
            ModelState.Remove("urun.UrunAcıklamasi");

            if (ModelState.IsValid)
            {
                Yorum dbyorum = ym.Find(x => x.ID == yorum.ID);
                dbyorum.DuzenlemeTarihi = DateTime.Now;
                dbyorum.aciklama = yorum.aciklama;
                dbyorum.musteri = musteri;
                dbyorum.urun = urun;
                dbyorum.isAktif = yorum.isAktif;

                int sonuc = ym.Update(dbyorum);
                if (sonuc > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(yorum);
        }

        // GET: Admin/Yorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = ym.Find(x => x.ID == id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: Admin/Yorum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yorum yorum = ym.Find(x => x.ID == id);
            ym.Delete(yorum);
            return RedirectToAction("Index");
        }


    }
}
