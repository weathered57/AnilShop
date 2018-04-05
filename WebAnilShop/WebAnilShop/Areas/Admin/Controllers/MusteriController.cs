uusing System;
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
    public class MusteriController : Controller
    {
        private MusteriManager m = new MusteriManager();

        // GET: Admin/Musteri
        public ActionResult Index()
        {
            return View(m.List());
        }

        // GET: Admin/Musteri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteri musteri = m.Find(x => x.ID == id);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            return View(musteri);
        }

        // GET: Admin/Musteri/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Musteri musteri)
        {


            if (ModelState.IsValid)
            {
                musteri.musteriImagae = "profil.png";
                m.Insert(musteri);

                return RedirectToAction("Index");
            }

            return View(musteri);
        }

        // GET: Admin/Musteri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteri musteri = m.Find(x => x.ID == id);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            return View(musteri);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                Musteri mus = m.Find(x => x.ID == musteri.ID);
                mus.isAdmin = musteri.isAdmin;
                mus.isAktif = musteri.isAktif;
                mus.Ad = musteri.Ad;
                mus.Soyad = musteri.Soyad;
                mus.KullaniciAdi = musteri.KullaniciAdi;
                mus.Sifre = musteri.Sifre;
                mus.Email = musteri.Email;
                m.Update(musteri);

                return RedirectToAction("Index");
            }
            return View(musteri);
        }

        // GET: Admin/Musteri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteri musteri = m.Find(x => x.ID == id);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            return View(musteri);
        }

        // POST: Admin/Musteri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musteri musteri = m.Find(x => x.ID == id);
            m.Delete(musteri);

            return RedirectToAction("Index");
        }



    }
}
