using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocationVoiture.Models;

namespace LocationVoiture.Controllers
{
    [Authorize]
    public class VoituresController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Voitures
        [Authorize]
        public ActionResult Index()
        {
            var voitures = db.Voitures.Include(v => v.ApplicationUser).Include(v => v.Marque).Include(v => v.Offre);
            return View(voitures.ToList());
        }

        [Authorize(Roles = "Owner")]
        public ActionResult OwnerCars()
        {
            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var voitures = db.Voitures.Where(x => x.UserId == user.Id).Include(v => v.ApplicationUser).Include(v => v.Marque).Include(v => v.Offre);
            return View(voitures.ToList());
        }

        [Authorize]
        public ActionResult Disponible(DateTime? date)
        {
            if (date != null)
                ViewBag.date = date;

            var voitures = db.Voitures.Include(v => v.ApplicationUser).Include(v => v.Marque).Include(v => v.Offre).ToList();
            List<Voiture> disponibles = new List<Voiture>();
            List<Voiture> reserver = new List<Voiture>();
            DateTime pick_up, date_return;
            if (date != null)
            {
                foreach (Voiture voiture in voitures)
                {
                    var reservartions = db.Reservations.Where(x => x.id_voiture == voiture.id_voiture).ToList();
                    foreach (Reservation res in reservartions)
                    {

                        pick_up = res.date_prise_en_charge;
                        date_return = res.date_retour;

                        if (date >= DateTime.Now && date <= date_return && date >= pick_up)
                        {
                            reserver.Add(voiture);
                        }

                    }
                }

                foreach (var res in reserver)
                {
                    voitures.Remove(res);
                }
                return View(voitures);
            }
            else
            {
                return View();

            }

        }

        // GET: Voitures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        [Authorize(Roles = "Owner")]
        // GET: Voitures/Create
        public ActionResult Create()
        {
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele");
            ViewBag.id_offre = new SelectList(db.Offres.Where(x => x.UserId == user.Id && x.date_expiration > DateTime.Now), "id_offre", "libele");
            return View();
        }

        [Authorize(Roles = "Owner")]
        // POST: Voitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Voiture voiture, HttpPostedFileBase carImage)
        {
            if (ModelState.IsValid)
            {
                string name = System.Web.HttpContext.Current.User.Identity.Name;
                ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
                voiture.UserId = user.Id;
                voiture.date_ajout = DateTime.Now;
                string path = Path.Combine(Server.MapPath("~/Uploads/cars"), carImage.FileName);
                carImage.SaveAs(path);
                voiture.photo = carImage.FileName;
                db.Voitures.Add(voiture);
                db.SaveChanges();
                return RedirectToAction("OwnerCars");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", voiture.UserId);
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele", voiture.id_marque);
            ViewBag.id_offre = new SelectList(db.Offres, "id_offre", "libele", voiture.id_offre);
            return View(voiture);
        }

        [Authorize(Roles = "Owner")]
        // GET: Voitures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", voiture.UserId);
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele", voiture.id_marque);
            ViewBag.id_offre = new SelectList(db.Offres.Where(x => x.UserId == user.Id && x.date_expiration > DateTime.Now), "id_offre", "libele", voiture.id_offre);
            return View(voiture);
        }

        [Authorize(Roles = "Owner")]
        // POST: Voitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Voiture voiture, HttpPostedFileBase carImage)
        {
            if (ModelState.IsValid)
            {
                if (carImage != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads/cars"), carImage.FileName);
                    carImage.SaveAs(path);
                    voiture.photo = carImage.FileName;
                }

                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OwnerCars");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", voiture.UserId);
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele", voiture.id_marque);
            ViewBag.id_offre = new SelectList(db.Offres, "id_offre", "libele", voiture.id_offre);
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }


        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voiture voiture = db.Voitures.Find(id);
            db.Voitures.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("OwnerCars");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
