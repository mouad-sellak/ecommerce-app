using LocationVoiture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminDashboard
        public ActionResult Index(int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            ViewBag.year = year;

            List<ApplicationUser> users = db.Users.ToList();
            List<Voiture> cars = db.Voitures.ToList();
            List<Reservation> res = db.Reservations.ToList();
            List<int> owners = new List<int>();
            List<int> tenants = new List<int>();
            List<int> months = new List<int>();
            List<int> cars_nb = new List<int>();
            List<int> res_nb = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                tenants.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "2")));
                owners.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "3")));
                cars_nb.Add(cars.Count(x => x.date_ajout.Year == year && x.date_ajout.Month == i));
                res_nb.Add(res.Count(x => x.date_ajout.Year == year && x.date_ajout.Month == i));

                months.Add(i);
            }

            ViewBag.months = months;
            ViewBag.tenants = tenants;
            ViewBag.cars = cars_nb;
            ViewBag.owners = owners;
            ViewBag.res = res_nb;
            return View();
        }

        /* public ActionResult MyChart()
         {

             List<ApplicationUser> users = db.Users.ToList();
             List<Voiture> cars = db.Voitures.ToList();
             List<int> owners = new List<int>();
             List<int> tenants = new List<int>();
             List<int> cars_nb = new List<int>();
             List<int> months = new List<int>();
             int year = DateTime.Now.Year;

             for (int i = 1; i <= 12; i++)
             {
                 tenants.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "2")));
                 owners.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "3")));
                 owners.Add(cars.Count(x => x.date_ajout.Year == year && x.date_ajout.Month == i ));

                 months.Add(i);
             }
             ViewBag.cars = cars;

             int[] xv = months.ToArray();

             int[] yv = tenants.ToArray();


             new System.Web.Helpers.Chart(width: 800, height: 200)
                 .AddSeries(
                 chartType: "Column",
                 xValue: xv,
                 yValues: yv,
                 axisLabel: "lala"

                 ).AddTitle("Nombre des étudiants par filière.")
                 .Write("png");

             return null;
         }
 */
    }
}