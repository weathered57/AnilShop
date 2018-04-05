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
using AnilShop.Entities.ViewsModel;

namespace AnilShop.Web.Controllers
{
    public class SepetController : Controller
    {
        private UrunManager um = new UrunManager();
        Cart cart = new Cart();

        public ActionResult sepeteEkle(int? id)
        {
            Urun urun = um.Find(x => x.ID == id);

            if (HttpContext.Session["sepet"] == null)
            {
                HttpContext.Session["sepet"] = new List<sepetItem>();
                cart.AddProducts(urun, 1);
            }
            else
            {
                cart.products = HttpContext.Session["sepet"] as List<sepetItem>;
                cart.AddProducts(urun, 1);
            }

            var sepettekiler = HttpContext.Session["sepet"] as List<sepetItem>;

            return View("Index", sepettekiler);
        }

        public ActionResult Index()
        {
            if (Session["login"] != null)
            {

                List<sepetItem> urunler = cart.listProduct();

                ViewBag.total = cart.Total();

                return View(urunler);
            }

            return View("Index", "Home");

        }
        public ActionResult delete(int? id)
        {
            if (Session["login"] != null)
            {
                Urun urun = um.Find(x => x.ID == id);
                cart.RemoveProduct(urun);
                List<sepetItem> urunler = cart.listProduct();


                return View("Index", urunler);
            }

            return View();
        }
        public ActionResult completeShopping()
        {
            return View();
        }


    }
}

