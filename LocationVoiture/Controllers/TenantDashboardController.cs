using LocationVoiture.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    
    [Authorize(Roles = "Tenant")]
    public class TenantDashboardController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Tenant
        public ActionResult Index()
        {
            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var reservations = db.Reservations.Where(x => x.UserId == user.Id).Include(r => r.ApplicationUser).Include(r => r.Paiement).Include(r => r.Voiture);
            return View(reservations.ToList());
        }
        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }
    }
}