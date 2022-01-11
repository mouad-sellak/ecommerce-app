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