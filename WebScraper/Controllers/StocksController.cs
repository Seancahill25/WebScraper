using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebScraper.Models;

namespace WebScraper.Controllers
{
    public class StocksController : Controller
    {
        private stock_portfolioEntities db = new stock_portfolioEntities();



        // GET: Stocks
        public ActionResult Index()
        {
            return View(db.Table_1.ToList());
        }

        public ActionResult Scrape()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Scrape([Bind(Include = "Symbol")] Table_1 table_1)
        {

            {
                // Create a new instance of the Chrome driver.
                var options = new ChromeOptions();
                options.AddArgument("--disable-gpu");
                options.AddArgument("--ignore-certificate-errors");
                options.AddArgument("--ignore-ssl-errors");
                var driver = new ChromeDriver();


                driver.Navigate().GoToUrl("https://login.yahoo.com");

                var username = driver.FindElement(By.Name("username"));
                username.SendKeys("seanscrap25");
                username.Submit();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(d => d.FindElement(By.Id("login-passwd")));

                var password = driver.FindElement(By.Id("login-passwd"));
                password.SendKeys("Potatosalad33");
                password.SendKeys(Keys.Enter);

                driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_1/view/v1");

                wait.Until(d => d.FindElement(By.Id("__dialog")));
                //var popup = driver.FindElement(By.XPath("//dialog[@id = '__dialog']/section/button"));
                //popup.Click();

                var table = driver.FindElement(By.CssSelector("#main > section > section._64nqq > div.gIc8M > table"));
                Console.WriteLine("potato");
                foreach (var row in table.FindElements(By.TagName("tr")))
                {
                    Console.WriteLine("another potato");
                    foreach (var cell in row.FindElements(By.TagName("td")))
                    {
               
                        Console.WriteLine(cell.Text);
                    }

                }
            }
            return RedirectToAction("Index");
        }
        // GET: Stocks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_1 table_1 = db.Table_1.Find(id);
            if (table_1 == null)
            {
                return HttpNotFound();
            }
            return View(table_1);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Symbol")] Table_1 table_1)
        {
            if (ModelState.IsValid)
            {
                db.Table_1.Add(table_1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_1);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_1 table_1 = db.Table_1.Find(id);
            if (table_1 == null)
            {
                return HttpNotFound();
            }
            return View(table_1);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Symbol")] Table_1 table_1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_1);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_1 table_1 = db.Table_1.Find(id);
            if (table_1 == null)
            {
                return HttpNotFound();
            }
            return View(table_1);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Table_1 table_1 = db.Table_1.Find(id);
            db.Table_1.Remove(table_1);
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
