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
    [Authorize(Roles = "Ownern,Admin")]
    public class MarquesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Marques
        public ActionResult Index()
        {
            return View(db.Marques.ToList());
        }

        // GET: Marques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        [Authorize(Roles = "Owner")]
        // GET: Marques/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Marques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Create(Marque marque)
        {
            if (ModelState.IsValid)
            {
                marque.date_ajout = DateTime.Now;
                db.Marques.Add(marque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marque);
        }

        [Authorize(Roles = "Owner")]
        // GET: Marques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: Marques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Edit([Bind(Include = "id_marque,libele,date_ajout")] Marque marque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marque);
        }

        [Authorize(Roles = "Owner")]
        // GET: Marques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: Marques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult DeleteConfirmed(int id)
        {
            Marque marque = db.Marques.Find(id);
            db.Marques.Remove(marque);
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
