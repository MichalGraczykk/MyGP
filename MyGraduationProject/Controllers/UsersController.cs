using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace MyGraduationProject.Controllers
{
    public class UsersController : Controller
    {
        private DataClassesDataContext db = new DataClassesDataContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role).Include(u => u.UsersAdress);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            //rok,miesiac,dzien
            var startDate = new DateTime(2016,06,05);
            var endDate = new DateTime(2016, 07, 02);
            //zapytanie zagniezdzone musi zwracac boola dla tego na koncu jest any(), any zwraca informacje czy wystepuje jakis element w liscie(jesli lista bedzie pusta to zwroci false w przeciwnym wypadku frue)

            // pobierze liste itemow dostepnych w chwili wywolania widoku
            var availableItems = db.Items
                .Where(i => i.Reservations
                    .Where(reservation =>
                        reservation.DATE_FROM > startDate && reservation.DATE_FROM > endDate 
                        || reservation.DATE_TO < startDate && reservation.DATE_TO < endDate).Any() 
                    || i.Reservations.Any()
                );


            //TODO rozkmin to
            //sprawdzi czy item na pewno jest dostepny w momencie stworzenia rezerwacji

            //powinno byc false
            var itemId = 2;
            var itemToBook = db.Items.Where(i => i.ITEM_ID == itemId).FirstOrDefault();
            var isAvailable = itemToBook.Reservations
                .Where(reservation =>
                    reservation.DATE_FROM > startDate && reservation.DATE_FROM > endDate
                    || reservation.DATE_TO < startDate && reservation.DATE_TO < endDate).Any()
                    || !itemToBook.Reservations.Any();


            //powinno byc true
            var itemId2 = 3;
            var itemToBook2 = db.Items.Where(i => i.ITEM_ID == itemId2).FirstOrDefault();
            var isAvailable2 = itemToBook2.Reservations
                .Where(reservation =>
                    reservation.DATE_FROM > startDate && reservation.DATE_FROM > endDate
                    || reservation.DATE_TO < startDate && reservation.DATE_TO < endDate).Any()
                    || !itemToBook2.Reservations.Any();


            //powinno byc true
            var itemId6 = 6;
            var itemToBook6 = db.Items.Where(i => i.ITEM_ID == itemId6).FirstOrDefault();
            var isAvailable6 = itemToBook6.Reservations
                .Where(reservation =>
                    reservation.DATE_FROM > startDate && reservation.DATE_FROM > endDate
                    || reservation.DATE_TO < startDate && reservation.DATE_TO < endDate).Any() 
                || !itemToBook6.Reservations.Any();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //FirstOrDefault() - dzieki temu zwroci tylko 1 rekord a jesli bedzie pusty to zwroci nulla
            //chce boola dla tego ze w where potrzebuje operatorow logicznych (==, <, !=) 
            var user = db.Users.Where(u => u.USER_ID == id).FirstOrDefault();

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
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
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
            var user = db.Users.Where(u => u.USER_ID == id).FirstOrDefault();
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
                var userToEdit = db.Users.Where(u => u.USER_ID == user.USER_ID).FirstOrDefault();
                userToEdit.LOGIN = user.LOGIN;
                userToEdit.PASSWORD = user.PASSWORD;
                userToEdit.NAME = user.NAME;
                userToEdit.SURNAME = user.SURNAME;
                userToEdit.AGE = user.AGE;
                userToEdit.UsersAdress.STREET_NAME = user.UsersAdress.STREET_NAME;
                userToEdit.UsersAdress.STREET_NUMBER = user.UsersAdress.STREET_NUMBER;
                userToEdit.UsersAdress.POSSESION_NUMBER = user.UsersAdress.POSSESION_NUMBER;
                userToEdit.ROLE_ID = user.ROLE_ID;

                db.SubmitChanges();
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
            var user = db.Users.Where(u => u.USER_ID == id).FirstOrDefault();
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
            var user = db.Users.Where(u => u.USER_ID == id).FirstOrDefault();
            var adress = user.UsersAdress;
            db.UsersAdresses.DeleteOnSubmit(adress);
            db.Users.DeleteOnSubmit(user);
            db.SubmitChanges();
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
