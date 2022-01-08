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
    public class BlackListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlackLists
        public ActionResult Index()
        {
            var blackLists = db.BlackLists.Include(b => b.ApplicationUser);
            return View(blackLists.ToList());
        }

        // GET: BlackLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
        }

        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Where(x => x.UserId.Equals(id)).FirstOrDefault();
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
            //return View("~/Views/ApplicationUsers/Index.cshtml");
        }

        // POST: FavoriteLists/Create/UserId
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed(string id)
        {
            BlackList blackList = db.BlackLists.Where(x => x.UserId.Equals(id)).FirstOrDefault();
            if (blackList == null)
            {
                blackList = new BlackList();
                blackList.UserId = id;
                db.BlackLists.Add(blackList);
                db.SaveChanges();
            }
            return Redirect(Url.Action("Index", "ApplicationUsers"));
            //return View("~/Views/ApplicationUsers/Index.cshtml");
        }

        // GET: BlackLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserType", blackList.UserId);
            return View(blackList);
        }

        // POST: BlackLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_favorite,UserId")] BlackList blackList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blackList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserType", blackList.UserId);
            return View(blackList);
        }

        // GET: BlackLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = db.BlackLists.Find(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
        }

        // POST: FavoriteLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlackList blackList = db.BlackLists.Find(id);
            db.BlackLists.Remove(blackList);
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
