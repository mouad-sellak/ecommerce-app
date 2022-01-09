using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        [Authorize]
        public ActionResult Index()
        {
            var products = db.Products.Include(v => v.ApplicationUser).Include(v => v.Marque).Include(v => v.Offre);
            return View(products.ToList());
        }

        [Authorize(Roles = "Owner")]
        public ActionResult OwnerCars()
        {
            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var products = db.Products.Where(x => x.UserId == user.Id).Include(v => v.ApplicationUser).Include(v => v.Marque).Include(v => v.Offre);
            return View(products.ToList());
        }

        [Authorize]
        public ActionResult Disponible(DateTime? date)
        {
            if (date != null)
                ViewBag.date = date;

            var products = db.Products.Include(v => v.ApplicationUser).Include(v => v.Marque).Include(v => v.Offre).ToList();
            List<Product> disponibles = new List<Product>();
            List<Product> reserver = new List<Product>();
            DateTime pick_up, date_return;
            if (date != null)
            {
                foreach (Product product in products)
                {
                    var reservartions = db.Reservations.Where(x => x.id_product == product.id_product).ToList();
                    foreach (Reservation res in reservartions)
                    {

                        pick_up = res.date_prise_en_charge;
                        date_return = res.date_retour;

                        if (date >= DateTime.Now && date <= date_return && date >= pick_up)
                        {
                            reserver.Add(product);
                        }

                    }
                }

                foreach (var res in reserver)
                {
                    products.Remove(res);
                }
                return View(products);
            }
            else
            {
                return View();

            }

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Owner")]
        // GET: Products/Create
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
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase carImage)
        {
            if (ModelState.IsValid)
            {
                string name = System.Web.HttpContext.Current.User.Identity.Name;
                ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
                product.UserId = user.Id;
                product.date_ajout = DateTime.Now;
                string path = Path.Combine(Server.MapPath("~/Uploads/cars"), carImage.FileName);
                carImage.SaveAs(path);
                product.photo = carImage.FileName;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("OwnerCars");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", product.UserId);
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele", product.id_marque);
            ViewBag.id_offre = new SelectList(db.Offres, "id_offre", "libele", product.id_offre);
            return View(product);
        }

        [Authorize(Roles = "Owner")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", product.UserId);
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele", product.id_marque);
            ViewBag.id_offre = new SelectList(db.Offres.Where(x => x.UserId == user.Id && x.date_expiration > DateTime.Now), "id_offre", "libele", product.id_offre);
            return View(product);
        }

        [Authorize(Roles = "Owner")]
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase carImage)
        {
            if (ModelState.IsValid)
            {
                if (carImage != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads/cars"), carImage.FileName);
                    carImage.SaveAs(path);
                    product.photo = carImage.FileName;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OwnerCars");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", product.UserId);
            ViewBag.id_marque = new SelectList(db.Marques, "id_marque", "libele", product.id_marque);
            ViewBag.id_offre = new SelectList(db.Offres, "id_offre", "libele", product.id_offre);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
