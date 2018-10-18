using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebScraper.Models;

namespace WebScraper.Controllers
{
    public class StocksController : Controller
    {
        private stock_portfolioEntities1 db = new stock_portfolioEntities1();



        // GET: Stocks
        public ActionResult Index()
        {
            return View(db.stocks.ToList());
        }

        public ActionResult Scrape()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Scrape([Bind(Include = "Symbol")] stock Table_1)
        {

            {
                // Create a new instance of the Chrome driver.
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--ignore-certificate-errors");
                options.AddArgument("--ignore-ssl-errors");
                var driver = new ChromeDriver(options);



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
                List<stock> scrapedData = new List<stock>();
                var table = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody"));
                var i = 1;
                foreach (var row in table.FindElements(By.TagName("tr")))
                {
                    stock tempStock = new stock
                    {
                        Symbol = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[1]/span/a")).GetAttribute("innerText"),
                        LastPrice = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[2]/span")).GetAttribute("innerText"),
                        Change = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[3]/span")).GetAttribute("innerText"),
                        PercentChg = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[4]/span")).GetAttribute("innerText"),
                        Currency = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[5]")).GetAttribute("innerText"),
                        MarketTime = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[6]/span")).GetAttribute("innerText"),
                        Volume = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[7]/span")).GetAttribute("innerText"),
                        AvgVol = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[9]")).GetAttribute("innerText"),
                        MarketCap = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[13]/span")).GetAttribute("innerText")
                    };

                    scrapedData.Add(tempStock);
                    i++; 
                    
                }
                driver.Quit();
                
                using (SqlConnection connection = new SqlConnection(@"data source=SEAN-PC\SQL;initial catalog=stock_portfolio;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))

                {
                          
                    connection.Open();
                    foreach (var stock in scrapedData)
                    {
                        db.stocks.Add(stock);
                    }
                    
                    db.SaveChanges();
                    connection.Close();
                  
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
            stock table_1 = db.stocks.Find(id);
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
        public ActionResult Create([Bind(Include = "Symbol")] stock table_1)
        {
            if (ModelState.IsValid)
            {
                db.stocks.Add(table_1);
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
            stock table_1 = db.stocks.Find(id);
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
        public ActionResult Edit([Bind(Include = "Symbol")] stock table_1)
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
            stock table_1 = db.stocks.Find(id);
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
            stock table_1 = db.stocks.Find(id);
            db.stocks.Remove(table_1);
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
