using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyGraduationProject.Models;

namespace MyGraduationProject.Controllers
{
    public class UsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role).Include(u => u.UsersAdress);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.ROLE_ID = new SelectList(db.Roles, "ROLE_ID", "NAME");
            ViewBag.ADRESS_ID = new SelectList(db.UsersAdresses, "ADRESS_ID", "STREET_NAME");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ROLE_ID = new SelectList(db.Roles, "ROLE_ID", "NAME", user.ROLE_ID);
            ViewBag.ADRESS_ID = new SelectList(db.UsersAdresses, "ADRESS_ID", "STREET_NAME", user.ADRESS_ID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ROLE_ID = new SelectList(db.Roles, "ROLE_ID", "NAME", user.ROLE_ID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user.UsersAdress).State = EntityState.Modified;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ROLE_ID = new SelectList(db.Roles, "ROLE_ID", "NAME", user.ROLE_ID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            UsersAdress adress = null;
            if (user.UsersAdress != null)
                adress = db.UsersAdresses.Find(user.UsersAdress.ADRESS_ID);
            db.Users.Remove(user);
            if(adress!=null)
                db.UsersAdresses.Remove(adress);
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
