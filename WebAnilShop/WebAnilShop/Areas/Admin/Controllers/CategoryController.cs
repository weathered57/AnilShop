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
    public class CategoryController : Controller
    {
        private KategoriManager cat = new KategoriManager();
        private UrunManager urun = new UrunManager();

        public ActionResult Index(int? id)
        {
            return View(cat.List());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = cat.Find(x => x.ID == id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                cat.Insert(kategori);
                return RedirectToAction("Index");
            }

            return View(kategori);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = cat.Find(x => x.ID == id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                Kategori dbKategori = cat.Find(x => x.ID == kategori.ID);
                dbKategori.KategoriAdi = kategori.KategoriAdi;
                cat.Update(kategori);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = cat.Find(x => x.ID == id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = cat.Find(x => x.ID == id);
            cat.Delete(kategori);
            return RedirectToAction("Index");
        }

    }
}
