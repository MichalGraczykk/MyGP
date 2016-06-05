using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyGraduationProject.Models;
using MyGraduationProject.Models.Enums;

namespace MyGraduationProject.Controllers
{
    public class AdminsController : Controller
    {
        //zmienna bezpośredniego dostępu do bazy danych
        private DataClassesDataContext db = new DataClassesDataContext();

        //zmienna dostępu do klasy MyRepository
        private MyRepository repo = new MyRepository();

        //widok o nazwie Index z folderu Admins wyświetli liste wszystkich użytkowników
        // GET: Admins
        public ActionResult Index()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    return View(repo.GetAllUsers());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    ViewBag.ROLE_ID = new SelectList(repo.GetAllRoles(), "ROLE_ID", "NAME");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Admins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (ModelState.IsValid)
                    {
                        //sprawdzamy czy podany login nie jest zajęty
                        var validation = repo.GetUserByLogin(user.LOGIN);
                        if (validation == null)
                        {
                            db.Users.InsertOnSubmit(user);
                            db.SubmitChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ten login jest juz zajęty");
                        }
                    }

                    ViewBag.ROLE_ID = new SelectList(repo.GetAllRoles(), "ROLE_ID", "NAME", user.ROLE_ID);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {

            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var user = repo.GetUserById((int)id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.ROLE_ID = new SelectList(repo.GetAllRoles(), "ROLE_ID", "NAME", user.ROLE_ID);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
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
                    ViewBag.ROLE_ID = new SelectList(repo.GetAllRoles(), "ROLE_ID", "NAME", user.ROLE_ID);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    var user = repo.GetUserById((int)id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    //TODO dorobic try catche zeby transakcje wykonywaly sie zaleznie od siebie 
                    var user = repo.GetUserById((int)id);
                    var adress = user.UsersAdress;
                    db.UsersAdresses.DeleteOnSubmit(adress);
                    db.Users.DeleteOnSubmit(user);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/Properties
        public ActionResult Properties()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        //GET: Admins/ListOfItems
        public ActionResult ListOfItems()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    return View(repo.GetAllItems());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/CreateItem
        public ActionResult CreateItem()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    ViewBag.STATE_ID = new SelectList(repo.GetAllStatuses(), "STATE_ID", "STATE_NAME");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        // POST: Admins/CreateItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem(Item item)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (ModelState.IsValid)
                    {
                        //sprawdzamy czy podany login nie jest zajęty
                        var validation = repo.GetUserByLogin(item.NAME);
                        if (validation == null)
                        {
                            db.Items.InsertOnSubmit(item);
                            db.SubmitChanges();
                            return RedirectToAction("ListOfItems", "Admins");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ten login jest juz zajęty");
                        }
                    }

                    ViewBag.STATE_ID = new SelectList(repo.GetAllStatuses(), "STATE_ID", "STATE_NAME", item.STATE_ID);
                    return View(item);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/EditItem/5
        public ActionResult EditItem(int? id)
        {

            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var item = repo.GetItemById((int)id);
                    if (item == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.STATE_ID = new SelectList(repo.GetAllStatuses(), "STATE_ID", "STATE_NAME", item.STATE_ID);
                    return View(item);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Admins/EditItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(Item item)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (ModelState.IsValid)
                    {
                        var itemToEdit = db.Items.Where(i => i.ITEM_ID == item.ITEM_ID).FirstOrDefault();
                        itemToEdit.NAME = item.NAME;
                        itemToEdit.DESCRPTION = item.DESCRPTION;
                        itemToEdit.PHOTO = item.PHOTO;
                        itemToEdit.PRICE_PER_DAY = item.PRICE_PER_DAY;
                        itemToEdit.STATE_ID = item.STATE_ID;

                        db.SubmitChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.STATE_ID = new SelectList(repo.GetAllStatuses(), "STATE_ID", "STATE_NAME", item.STATE_ID);
                    return View(item);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        //GET: Admins/ListOfReservations
        public ActionResult ListOfReservations()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    ViewBag.STATE_ID = new SelectList(repo.GetAllReservationStatuses(), "STATUS_ID", "NAME");
                    return View(repo.GetAllReservations());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/Details/5
        public ActionResult DetailsOfReservation(int? id)
        {
            //TODO przerobic dla itemow i do wyswietlania uzytkownika transakcji
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    var reservation = repo.GetReservationById((int)id);

                    if (reservation == null)
                    {
                        return HttpNotFound();
                    }

                    ViewBag.STATUS_ID = new SelectList(repo.GetAllReservationStatuses(), "STATUS_ID", "NAME", reservation.STATUS_ID);
                    return View(reservation);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }


        // POST: Admins/Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsOfReservation(Reservation reservation)
        {
            //TODO przerobic dla itemow i do wyswietlania uzytkownika transakcji
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (ModelState.IsValid)
                    {
                        if (reservation == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }

                        var reservationToEdit = db.Reservations.Where(r => r.RESERVATION_ID == reservation.RESERVATION_ID).FirstOrDefault();

                        if (reservationToEdit == null)
                        {
                            return HttpNotFound();
                        }

                        reservationToEdit.STATUS_ID = reservation.STATUS_ID;

                        db.SubmitChanges();

                        return RedirectToAction("ListOfReservations", "Admins");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
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

/*
 * 
 *             //rok,miesiac,dzien
            var startDate = new DateTime(2016, 06, 05);
            var endDate = new DateTime(2016, 07, 02);

            var listOfAvailableItems = new List<Item>();
            // pobierze liste itemow dostepnych w chwili wywolania widoku
            listOfAvailableItems.AddRange(repo.GetListOfAvailableItems(startDate, endDate));

            //dostępnosc produktu podczas tworzenia rezerwacji
            //TODO isAvailable powinno byc false dla 2, dla 3,6 true
            var itemId = 6;
            var isAvailable = repo.isItemAvailable(itemId, startDate, endDate);
 * 
 */
