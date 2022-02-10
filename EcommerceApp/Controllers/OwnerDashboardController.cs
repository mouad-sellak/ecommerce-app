using EcommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceApp.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnerDashboardController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Owner
        public ActionResult Index(int? year)
        {
            ViewBag.year = year;
            if(year==null) year= DateTime.Now.Year;
            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var products = db.Products.Where(x => x.UserId == user.Id && x.date_ajout.Year==year).GroupBy(x=>x.date_ajout.Month);
            var products_Count = products.Select(x=>x.Count());
            var prices=products.Select(x => new { Amount = x.Sum(b => b.prix) }).Select(p=>((int)p.Amount));
            ViewBag.products_Count =products_Count; 
            ViewBag.prices = prices;
            return View();
        }

        public ActionResult historique()
        {
            HistoryOwner history = new HistoryOwner();
            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            history.productsDisponibles = db.Products.Where(x => x.UserId == user.Id).Include(v => v.ApplicationUser).ToList<Product>();
            history.offresDisponibles = db.Offres.Where(x => x.UserId == user.Id && x.date_expiration >= DateTime.Now).Include(v => v.ApplicationUser).ToList<Offre>();
            history.offresExpires = db.Offres.Where(x => x.UserId == user.Id && x.date_expiration < DateTime.Now).Include(v => v.ApplicationUser).ToList<Offre>();

            return View(history);
        }

    }
}