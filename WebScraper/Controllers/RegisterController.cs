using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScraper.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace WebScraper.Controllers
{
    public class RegisterController : Controller
    {
        private stock_portfolioEntities2 db = new stock_portfolioEntities2();

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username,Email,Password")] User user)
        {
            user = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            using (SqlConnection connection = new SqlConnection(@"data source=SEAN-PC\SQL;initial catalog=stock_portfolio;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                connection.Open();
                db.Users.Add(user);
                db.SaveChanges();
                connection.Close();
            }

                return RedirectToAction("Index");
        }
    }
}