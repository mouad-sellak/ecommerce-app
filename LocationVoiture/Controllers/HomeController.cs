using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.Web;
using LocationVoiture.Models;

namespace LocationVoiture.Controllers
{
    public class HomeController : BaseController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string lang)
        {
            if (lang != null)
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

            string name = User.Identity.Name;
            ViewBag.name = name;

            List<Voiture> voitures = db.Voitures.ToList();
            return View(voitures);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new GestionLanguages().SetLanguage(lang);
            return Redirect(Request.UrlReferrer.ToString());
            /* obtient les informations sur l'URL de la précédente requête du client
            qui était liée a la requête actuelle */
        }
    }
}