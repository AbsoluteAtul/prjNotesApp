using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prjNotesApp;
using prjNotesApp.Models;

namespace prjNotesApp.Controllers
{
    public class RegisterController : Controller
    {
        private dbNotesEntities db = new dbNotesEntities();
        UserProfile user = new UserProfile();
        // GET: Register
        public ActionResult Index()
        {

            return View(db.tabLogins.ToList());
        }

        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabLogin tabLogin = db.tabLogins.Find(id);
            if (tabLogin == null)
            {
                return HttpNotFound();
            }
            return View(tabLogin);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password")] tabLogin tabLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tabLogins.Add(tabLogin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ViewBag.Error += ViewBag.Error + "\n" + ve.ErrorMessage;
                    }
                }
                
            }
            catch (DbUpdateException e)
            {
                //Add your code to inspect the inner exception and/or
                //e.Entries here.
                //Or just use the debugger.
                //Added this catch (after the comments below) to make it more obvious 
                //how this code might help this specific problem
                ViewBag.Error += ViewBag.Error + "\n" + e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Error += ViewBag.Error + "\n" + e.InnerException.Message;
            }
            return View(tabLogin);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabLogin tabLogin = db.tabLogins.Find(id);
            if (tabLogin == null)
            {
                return HttpNotFound();
            }
            return View(tabLogin);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password")] tabLogin tabLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabLogin);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
          
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tabLogin tabLogin = db.tabLogins.Find(id);
                if (tabLogin == null)
                {
                    return HttpNotFound();
                }
                return View(tabLogin);
           
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
            {
                tabLogin tabLogin = db.tabLogins.Find(id);
                db.tabLogins.Remove(tabLogin);
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
        private void Authenticate(string returnUrl)
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            //if the user didnt logged in the cookie will be null 
            if (cookie == null)
            {
                Response.Redirect("/Login/Index?ReturnUrl=" + returnUrl, false);
            }
        }
    }
}
