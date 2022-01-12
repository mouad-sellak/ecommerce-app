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
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.Where(x => x.UserType.Equals("Owner")).ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        public ActionResult historique(string id)
        {
            HistoryOwner history = new HistoryOwner();
            var user = db.Users.Find(id);
            history.offresDisponibles = db.Offres.Where(x => x.UserId == user.Id && x.date_expiration >= DateTime.Now).Include(v => v.ApplicationUser).ToList<Offre>();
            history.productsDisponibles = db.Products.Where(x => x.UserId == user.Id).Include(v => v.ApplicationUser).ToList<Product>();
            history.offresExpires = db.Offres.Where(x => x.UserId == user.Id && x.date_expiration < DateTime.Now).Include(v => v.ApplicationUser).ToList<Offre>();

            return View(history);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteList favoriteList = db.FavoriteLists.Where(x => x.UserId.Equals(id)).FirstOrDefault();
            if (favoriteList == null)
            {
                return HttpNotFound();
            }
            return View(favoriteList);
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed(string id)
        {
            FavoriteList favoriteList = db.FavoriteLists.Where(x => x.UserId.Equals(id)).FirstOrDefault();
            if (favoriteList == null)
            {
                favoriteList = new FavoriteList();
                favoriteList.UserId = id;
                db.FavoriteLists.Add(favoriteList);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
            //return View("~/Views/ApplicationUsers/Index.cshtml");
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserType,UserAdress,date_join,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if(applicationUser.blocked==true)
                applicationUser.blocked = false;
            else
                applicationUser.blocked = true;
            db.Entry(applicationUser).State = EntityState.Modified;
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
