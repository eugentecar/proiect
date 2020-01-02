using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proiect_pssc.Models;

namespace proiect_pssc.Controllers
{
    public class LoginnnsController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();

        // GET: Loginnns
        public ActionResult Index()
        {
            return View(db.Loginuri.ToList());
        }

        // GET: Loginnns/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loginnn loginnn = db.Loginuri.Find(id);
            if (loginnn == null)
            {
                return HttpNotFound();
            }
            return View(loginnn);
        }

        // GET: Loginnns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loginnns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Parola")] Loginnn loginnn)
        {
            if (ModelState.IsValid)
            {
                db.Loginuri.Add(loginnn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginnn);
        }

        // GET: Loginnns/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loginnn loginnn = db.Loginuri.Find(id);
            if (loginnn == null)
            {
                return HttpNotFound();
            }
            return View(loginnn);
        }

        // POST: Loginnns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Parola")] Loginnn loginnn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginnn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginnn);
        }

        // GET: Loginnns/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loginnn loginnn = db.Loginuri.Find(id);
            if (loginnn == null)
            {
                return HttpNotFound();
            }
            return View(loginnn);
        }

        // POST: Loginnns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Loginnn loginnn = db.Loginuri.Find(id);
            db.Loginuri.Remove(loginnn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Loginnn account)
        {
            if (ModelState.IsValid) {

                using(BibliotecaContext db = new BibliotecaContext())
                {
                    db.Loginuri.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
             //   ViewBag.Message= account.ProductId + " " +

            }

            return View();
        }


        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost] //aici imi vin valorile celor 2 textboxuri din pagina de login
        public ActionResult Logina(Loginnn home)
        {
            string product = home.ProductId;
            string pass = home.Parola;

            //sal
            return View();
        }








        [HttpPost]
        public ActionResult Login(Loginnn account)
        {
            using (BibliotecaContext db = new BibliotecaContext())
            {
                var usr = db.Loginuri.Single(u => u.ProductId == account.ProductId && u.Parola == u.Parola);
                if(usr != null)
                {
                    Session["ProductId"] = usr.ProductId.ToString();
                    Session["Parola"] = usr.Parola.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Ceva e gresit");

                }
            }
            return View();

        }

        public ActionResult LoggedIn()
        {
            if (Session["ProductId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

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
