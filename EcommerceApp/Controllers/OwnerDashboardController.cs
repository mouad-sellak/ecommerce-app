using EcommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceApp.Controllers
{
   /* [Authorize(Roles = "Owner")]*/
    public class OwnerDashboardController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Owner
        public ActionResult Index(int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            ViewBag.year = year;

            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            /*--STATISTICS START--*/
            List<ApplicationUser> users = db.Users.ToList();
            List<Product> products = db.Products.ToList();
            List<Reservation> res = db.Reservations.ToList();
            List<int> months = new List<int>();
            List<int> tenants = new List<int>();
            List<int> products_nb = new List<int>();
            List<int> res_nb = new List<int>();

            List<float> prices = new List<float>();

            for (int i = 1; i <= 12; i++)
            {
                tenants.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "2")));
                products_nb.Add(products.Count(x => x.date_ajout.Year == year && x.date_ajout.Month == i && x.UserId == user.Id));
                res_nb.Add(res.Count(x => x.date_ajout.Year == year && x.date_ajout.Month == i && x.Product.UserId == user.Id));
                prices.Add(res.Where(x => x.date_ajout.Year == year && x.date_ajout.Month == i && x.Product.UserId == user.Id).Select(x => x.prix).Sum());
                months.Add(i);
            }

            ViewBag.months = months;
            ViewBag.products = products_nb;
            ViewBag.res = res_nb;
            ViewBag.tenants = tenants;
            ViewBag.prices = prices;
            /*-- STATISTICS END--*/
            return View();
        }

        public ActionResult historique()
        {
            HistoryOwner history = new HistoryOwner();
            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            history.reservations = db.Reservations.Where(x => x.Product.ApplicationUser.Id == user.Id).Include(r => r.ApplicationUser).Include(r => r.Paiement).Include(r => r.Product).ToList<Reservation>();
            history.offresDisponibles = db.Offres.Where(x => x.UserId == user.Id && x.date_expiration >= DateTime.Now).Include(v => v.ApplicationUser).ToList<Offre>();
            history.offresExpires = db.Offres.Where(x => x.UserId == user.Id && x.date_expiration < DateTime.Now).Include(v => v.ApplicationUser).ToList<Offre>();

            return View(history);
        }

    }
}