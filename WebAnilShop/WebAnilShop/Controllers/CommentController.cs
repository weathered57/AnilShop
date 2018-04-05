using AnilShop.BusinessLayer;
using AnilShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AnilShop.Web.Controllers
{
    public class CommentController : Controller
    {
        private UrunManager um = new UrunManager();
        private YorumManager ym = new YorumManager();
        // GET: Comment
        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Note note = noteManager.Find(x => x.Id == id);
            Urun urun = um.Find(x => x.ID == id);

            if (urun == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComment", urun.Yorumlar);
        }
        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum = ym.Find(x => x.ID == id);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }

            yorum.aciklama = text;
            yorum.DuzenlemeTarihi = DateTime.Now;

            if (ym.Update(yorum) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum = ym.Find(x => x.ID == id);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }

            if (ym.Delete(yorum) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(Yorum yorum, int? noteid)
        {
            Musteri musteri = Session["login"] as Musteri;
            if (ModelState.IsValid)
            {
                if (noteid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Urun urun = um.Find(x => x.ID == noteid);

                if (urun == null)
                {
                    return new HttpNotFoundResult();
                }

                yorum.urun = urun;
                yorum.musteri = musteri;


                if (ym.Insert(yorum) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

    }
}