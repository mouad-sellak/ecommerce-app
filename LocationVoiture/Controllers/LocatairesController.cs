using LocationVoiture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    [Authorize(Roles = "Owner")]
    public class LocatairesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Locataires
        public ActionResult Index()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = db.Users.Where(x => x.UserType.Equals("Tenant")).ToList();
            List<Locataire> locataires = new List<Locataire>();

            foreach(var user in users)
            {
                Locataire locataire = new Locataire() {
                email = user.Email,
                phone = user.PhoneNumber,
                address = user.UserAdress,
                user_name = user.UserName,
                date_ajout = user.date_join
                };

                locataires.Add(locataire);
            }
            return View(locataires);
        }

        // GET: Locataires/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Locataires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locataires/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locataires/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locataires/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locataires/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locataires/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
