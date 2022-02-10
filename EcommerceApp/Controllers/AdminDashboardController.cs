using EcommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminDashboard
        public ActionResult Index(int? year)
        {
            ViewBag.year = year;
            if (year == null) year = DateTime.Now.Year;
            var products = db.Products.Where(x=>x.date_ajout.Year == year).GroupBy(x => x.date_ajout.Month);
            var products_Count = products.Select(x => x.Count());
            var users = db.Users.Where(u=>u.date_join.Year==year).GroupBy(u=>u.date_join.Month).Select(x => x.Count());
            ViewBag.products_Count = products_Count;
            ViewBag.users = users;
            return View();
        }

        /* public ActionResult MyChart()
         {

             List<ApplicationUser> users = db.Users.ToList();
             List<Product> products = db.Products.ToList();
             List<int> owners = new List<int>();
             List<int> tenants = new List<int>();
             List<int> products_nb = new List<int>();
             List<int> months = new List<int>();
             int year = DateTime.Now.Year;

             for (int i = 1; i <= 12; i++)
             {
                 tenants.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "2")));
                 owners.Add(users.Count(x => x.date_join.Year == year && x.date_join.Month == i && x.Roles.Any(s => s.RoleId == "3")));
                 owners.Add(products.Count(x => x.date_ajout.Year == year && x.date_ajout.Month == i ));

                 months.Add(i);
             }
             ViewBag.products = products;

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