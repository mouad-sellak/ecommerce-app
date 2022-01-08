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
    public class ReclamationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reclamations
        public ActionResult Index()
        {
            var reclamations = db.Reclamations.Include(r => r.ApplicationUser);
            return View(reclamations.ToList());
        }

        // GET: Reclamations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclamation = db.Reclamations.Find(id);
            if (reclamation == null)
            {
                return HttpNotFound();
            }
            return View(reclamation);
        }

        // GET: Reclamations/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Reclamations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_reclamation,UserId,description,date_ajout")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                db.Reclamations.Add(reclamation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", reclamation.UserId);
            return View(reclamation);
        }

        // GET: Reclamations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclamation = db.Reclamations.Find(id);
            if (reclamation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", reclamation.UserId);
            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reclamation,UserId,description,date_ajout")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclamation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", reclamation.UserId);
            return View(reclamation);
        }

        // GET: Reclamations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclamation = db.Reclamations.Find(id);
            if (reclamation == null)
            {
                return HttpNotFound();
            }
            return View(reclamation);
        }

        // POST: Reclamations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamation reclamation = db.Reclamations.Find(id);
            db.Reclamations.Remove(reclamation);
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
