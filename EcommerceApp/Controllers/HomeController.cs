using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.Web;
using EcommerceApp.Models;
using System.Net;

namespace EcommerceApp.Controllers
{
    public class HomeController : BaseController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string lang,string category)
        {
            List<Product> products;
            if (lang != null)
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
            if (category != null)
            {

                products = db.Products.Where(p => p.Category.libele.Equals(category)).ToList();
            }
            else
            {
                products = db.Products.ToList();
            }

            return View(products);
        }
        [HttpPost]
        public ActionResult Search(string search)
        {
            List<Product> products =new List<Product>();
                products = db.Products.Where(p=> p.title.Contains(search) || p.location.Contains(search)
                  || p.description.Contains(search) || p.Category.libele.Contains(search)).ToList();

            ViewBag.name = search;

            return View("Index",products);
        }
        public ActionResult Offers(string lang, string category)
        {
            List<Product> products;
            if (lang != null)
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
            if (category != null)
            {

                products = db.Products.Where( p => p.Category.libele.Equals(category) ).OrderBy(p=>p.Offre.taux_remise).ThenByDescending(p => p.Offre.taux_remise).ToList();
            }
            else
            {
                products = (List<Product>)db.Products.OrderBy(p => p.Offre.taux_remise).ThenByDescending(p => p.Offre.taux_remise).ToList();
            }

            products.Reverse();

            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new GestionLanguages().SetLanguage(lang);
            return Redirect(Request.UrlReferrer.ToString());
            /* obtient les informations sur l'URL de la précédente requête du client
            qui était liée a la requête actuelle */
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public static List<Category> getCategories()
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            return DB.Categories.ToList();
        }
    }
}