using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prjNotesApp;

namespace prjNotesApp.Controllers
{
    public class NoteController : Controller
    {
        private dbNotesEntities db = new dbNotesEntities();

        // GET: Note
        public ActionResult Index()
        {
           
                 Authenticate("Note/Index");
           
                    var tabNotes = db.tabNotes.Include(t => t.tabLogin);
                    return View(tabNotes.ToList());
                
        }


        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            Authenticate("Note/Details");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabNote tabNote = db.tabNotes.Find(id);
            if (tabNote == null)
            {
                return HttpNotFound();
            }
            return View(tabNote);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            Authenticate("Note/Create");
            ViewBag.userid = new SelectList(db.tabLogins, "id", "username");
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "number,title,contents,userid")] tabNote tabNote)
        {
            Authenticate("Note/Create");
            try
            {
                if (ModelState.IsValid)
                {
                    db.tabNotes.Add(tabNote);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Handle = e.Message;
            }
            ViewBag.userid = new SelectList(db.tabLogins, "id", "username", tabNote.userid);
            return View(tabNote);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {
            Authenticate("Note/Edit");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabNote tabNote = db.tabNotes.Find(id);
            if (tabNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.tabLogins, "id", "username", tabNote.userid);
            return View(tabNote);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number,title,contents,userid")] tabNote tabNote)
        {
            Authenticate("Note/Edit");
            if (ModelState.IsValid)
            {
                db.Entry(tabNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.tabLogins, "id", "username", tabNote.userid);
            return View(tabNote);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            Authenticate("Note/Delete");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tabNote tabNote = db.tabNotes.Find(id);
            if (tabNote == null)
            {
                return HttpNotFound();
            }
            return View(tabNote);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Authenticate("Note/DeleteConfirmed");
            tabNote tabNote = db.tabNotes.Find(id);
            db.tabNotes.Remove(tabNote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Authenticate("Note/Dispose");
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
                Response.Redirect("/Login/Index?ReturnUrl=" + returnUrl ,false);
            }
        }
    }
}
