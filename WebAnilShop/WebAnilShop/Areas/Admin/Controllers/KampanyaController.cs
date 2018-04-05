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
    public class KampanyaController : Controller
    {
        KampanyaManager kam = new KampanyaManager();

        // GET: Admin/Kampanya
        public ActionResult Index()
        {
            return View(kam.List());
        }

        // GET: Admin/Kampanya/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kampanya kampanya = kam.Find(x => x.ID == id);
            if (kampanya == null)
            {
                return HttpNotFound();
            }
            return View(kampanya);
        }

        // GET: Admin/Kampanya/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kampanya kampanya)
        {
            if (ModelState.IsValid)
            {
                kam.Insert(kampanya);

                return RedirectToAction("Index");
            }

            return View(kampanya);
        }

        // GET: Admin/Kampanya/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kampanya kampanya = kam.Find(x => x.ID == id);
            if (kampanya == null)
            {
                return HttpNotFound();
            }
            return View(kampanya);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kampanya kampanya)
        {
            if (ModelState.IsValid)
            {
                Kampanya k = kam.Find(x => x.ID == kampanya.ID);
                k.kampanyaAdi = kampanya.kampanyaAdi;
                k.kampanyaYuzdesi = kampanya.kampanyaYuzdesi;

                kam.Update(k);

                return RedirectToAction("Index");
            }
            return View(kampanya);
        }

        // GET: Admin/Kampanya/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kampanya kampanya = kam.Find(x => x.ID == id);
            if (kampanya == null)
            {
                return HttpNotFound();
            }
            return View(kampanya);
        }

        // POST: Admin/Kampanya/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kampanya kampanya = kam.Find(x => x.ID == id);
            kam.Delete(kampanya);

            return RedirectToAction("Index");
        }


    }
}
