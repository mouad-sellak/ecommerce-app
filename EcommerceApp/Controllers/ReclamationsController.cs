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
    public class CategorysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorys
        public ActionResult Index()
        {
            var Categorys = db.Categorys.Include(r => r.ApplicationUser);
            return View(Categorys.ToList());
        }

        // GET: Categorys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Categorys.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Category,UserId,description,date_ajout")] Category Category)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", Category.UserId);
            return View(Category);
        }

        // GET: Categorys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Categorys.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", Category.UserId);
            return View(Category);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Category,UserId,description,date_ajout")] Category Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", Category.UserId);
            return View(Category);
        }

        // GET: Categorys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Categorys.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        // POST: Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category Category = db.Categorys.Find(id);
            db.Categorys.Remove(Category);
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
