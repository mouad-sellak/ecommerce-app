using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    public class ReclamationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reclamations
        public ActionResult Index()
        {
            var Reclamations = db.Reclamations.Include(r => r.ApplicationUser);
            return View(Reclamations.ToList());
        }

        // GET: Reclamations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation Reclamation = db.Reclamations.Find(id);
            if (Reclamation == null)
            {
                return HttpNotFound();
            }
            return View(Reclamation);
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
        public ActionResult Create([Bind(Include = "id_Reclamation,UserId,description,date_ajout")] Reclamation Reclamation)
        {
            if (ModelState.IsValid)
            {
                db.Reclamations.Add(Reclamation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", Reclamation.UserId);
            return View(Reclamation);
        }

        // GET: Reclamations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation Reclamation = db.Reclamations.Find(id);
            if (Reclamation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", Reclamation.UserId);
            return View(Reclamation);
        }

        // POST: Reclamations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Reclamation,UserId,description,date_ajout")] Reclamation Reclamation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Reclamation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", Reclamation.UserId);
            return View(Reclamation);
        }

        // GET: Reclamations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation Reclamation = db.Reclamations.Find(id);
            if (Reclamation == null)
            {
                return HttpNotFound();
            }
            return View(Reclamation);
        }

        // POST: Reclamations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamation Reclamation = db.Reclamations.Find(id);
            db.Reclamations.Remove(Reclamation);
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
