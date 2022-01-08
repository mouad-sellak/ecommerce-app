using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocationVoiture.Models;

namespace LocationVoiture.Controllers
{
    public class OffresController :  BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Offres
        public ActionResult Index()
        {
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if(User.IsInRole("Owner"))
            {
                var offres = db.Offres.Where(x => x.UserId == user.Id).Include(v => v.ApplicationUser);
                return View(offres.ToList());
            }
            else
            {
                var offres = db.Offres.Include(v => v.ApplicationUser);
                return View(offres.ToList());
            }
            
        }

        // GET: Offres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // GET: Offres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Offre offre)
        {
            if (ModelState.IsValid)
            {
                string name = System.Web.HttpContext.Current.User.Identity.Name;
                ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
                offre.UserId = user.Id;
                offre.date_ajout= DateTime.Now;
                db.Offres.Add(offre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offre);
        }

        // GET: Offres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", offre.UserId);
            return View(offre);
        }

        // POST: Offres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offre offre)
        {
            if (ModelState.IsValid)
            {
                Offre offreChange = db.Offres.Find(offre.id_offre);
                offreChange.libele = offre.libele;
                offreChange.taux_remise = offre.taux_remise;
                offreChange.date_expiration = Convert.ToDateTime(offre.date_expiration);
                db.Entry(offreChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", offre.UserId);
            return View(offre);
        }

        // GET: Offres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offre offre = db.Offres.Find(id);
            db.Offres.Remove(offre);
            db.SaveChanges();
            return RedirectToAction("Index");
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
