using DatabaseAccess;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyGraduationProject.Models;
using MyGraduationProject.Models.Enums;
using PagedList;

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
        public ActionResult Index(string sorting , string filtrationLogin, int? page)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    ViewBag.SortedBy = sorting;
                    ViewBag.SortByNAME = sorting == null ? "NAME_Malejaco" : "";
                    ViewBag.SortBySURNAME = sorting == "SURNAME_Malejaco" ? "SURNAME_Rosnaco" : "SURNAME_Malejaco";

                    var users = repo.GetAllUsers();

                    if (ModelState.IsValid)
                    {
                        if (filtrationLogin != null && filtrationLogin != "")
                        {
                            users = users.Where(u => u.LOGIN.Contains(filtrationLogin));
                        }

                        ViewBag.FindLogin = filtrationLogin;
                    

                        switch (sorting)
                        {
                            case "NAME_Malejaco":
                                users = users.OrderByDescending(s => s.NAME);
                                break;
                            case "SURNAME_Malejaco":
                                users = users.OrderByDescending(s => s.SURNAME);
                                break;
                            case "SURNAME_Rosnaco":
                                users = users.OrderBy(s => s.SURNAME);
                                break;
                            default:
                                users = users.OrderBy(s => s.NAME);
                                break;
                        }

                        int pageSize = 5;
                        int pageNumber = (page ?? 1);
                        return View(users.ToPagedList(pageNumber, pageSize));
                    }
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
                        var validation = repo.FreeLoginConfirm(user.LOGIN);
                        if (validation == false)
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
                            userToEdit.PESEL = user.PESEL;
                            userToEdit.UsersAdress.STREET_NAME = user.UsersAdress.STREET_NAME;
                            userToEdit.UsersAdress.STREET_NUMBER = user.UsersAdress.STREET_NUMBER;
                            userToEdit.UsersAdress.POSSESION_NUMBER = user.UsersAdress.POSSESION_NUMBER;
                            userToEdit.ROLE_ID = user.ROLE_ID;
                            db.SubmitChanges();

                        if (current.USER_ID == userToEdit.USER_ID)
                        {
                            Session["principal"] = userToEdit;
                        }
                        ViewBag.Auth = (User)(Session["principal"]);
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

        //GET: Admins/ListOfItems
        public ActionResult ListOfItems(string sorting, string filtrationNAME, int? page)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    ViewBag.SortedBy = sorting;
                    ViewBag.SortByNAME = sorting == null ? "NAME_Malejaco" : "";
                    ViewBag.SortByPRICE_PER_DAY = sorting == "PRICE_PER_DAY_Malejaco" ? "PRICE_PER_DAY_Rosnaco" : "PRICE_PER_DAY_Malejaco";
                    
                    if (ModelState.IsValid)
                    { 
                        var items = repo.GetAllItems();

                        if (filtrationNAME != null && filtrationNAME != "")
                        {
                            items = items.Where(i => i.NAME.Contains(filtrationNAME));
                        }

                        ViewBag.FindNAME = filtrationNAME;

                        switch (sorting)
                        {
                            case "NAME_Malejaco":
                                items = items.OrderByDescending(s => s.NAME);
                                break;
                            case "PRICE_PER_DAY_Malejaco":
                                items = items.OrderByDescending(s => s.PRICE_PER_DAY);
                                break;
                            case "PRICE_PER_DAY_Rosnaco":
                                items = items.OrderBy(s => s.PRICE_PER_DAY);
                                break;
                            default:
                                items = items.OrderBy(s => s.NAME);
                                break;
                        }

                        int pageSize = 5;
                        int pageNumber = (page ?? 1);
                        return View(items.ToPagedList(pageNumber, pageSize));
                    }
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
                        //sprawdzamy czy podana nazwa nie jest zajęta
                        var validation = repo.GetUserByLogin(item.NAME);
                        if (validation == null)
                        {
                            db.Items.InsertOnSubmit(item);
                            db.SubmitChanges();
                            return RedirectToAction("ListOfItems", "Admins");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ten przedmiot juz istnieje");
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
                        return RedirectToAction("ListOfItems", "Admins");
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
        public ActionResult ListOfReservations(string sorting, string BOOKINGSTATUS, int? page)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    ViewBag.SortedBy = sorting;
                    ViewBag.SortByDATE_FROM = sorting == null ? "DATE_FROM" : "";
                    ViewBag.SortByORDER_DATE = sorting == "ORDER_DATE_Malejaco" ? "ORDER_DATE_Rosnaco" : "ORDER_DATE_Malejaco";

                    if (ModelState.IsValid)
                    {
                        var reservations = repo.GetAllReservations();

                        if (BOOKINGSTATUS != null && BOOKINGSTATUS != "")
                        {
                            reservations = reservations.Where(r => r.ReservationStatuse.NAME.Contains(BOOKINGSTATUS));
                        }

                        ViewBag.FindBOOKINGSTATUS = BOOKINGSTATUS;

                        switch (sorting)
                        {
                            case "DATE_FROM_Malejaco":
                                reservations = reservations.OrderByDescending(s => s.DATE_FROM);
                                break;
                            case "ORDER_DATE_Malejaco":
                                reservations = reservations.OrderByDescending(s => s.ORDER_DATE);
                                break;
                            case "ORDER_DATE_Rosnaco":
                                reservations = reservations.OrderBy(s => s.ORDER_DATE);
                                break;
                            default:
                                reservations = reservations.OrderBy(s => s.DATE_FROM);
                                break;
                        }
                        ViewBag.STATE_ID = new SelectList(repo.GetAllReservationStatuses(), "STATUS_ID", "NAME");
                        int pageSize = 5;
                        int pageNumber = (page ?? 1);
                        return View(reservations.ToPagedList(pageNumber, pageSize));
                    }
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

                        if (reservationToEdit.STATUS_ID == (int)ReservationStatusesEnum.CANCELLED && repo.isItemAvailable((int)reservationToEdit.ITEM_ID, reservationToEdit.DATE_FROM, reservationToEdit.DATE_TO))
                        {
                            reservationToEdit.STATUS_ID = reservation.STATUS_ID;
                            db.SubmitChanges();
                        }
                        else if(reservationToEdit.STATUS_ID != (int)ReservationStatusesEnum.CANCELLED)
                        {
                            reservationToEdit.STATUS_ID = reservation.STATUS_ID;
                            db.SubmitChanges();
                        }
                            
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

        // GET: Admins/ListOfProperties
        public ActionResult ListOfProperties()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    var props = db.Properties;
                    return View(props);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ListOfProperties(string newProp)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (newProp != null && newProp != "")
                    {
                        var check = db.Properties.Where(u => u.NAME == newProp).FirstOrDefault();

                        if (check == null)
                        {
                            var tmp = new Property();
                            tmp.NAME = newProp;
                            db.Properties.InsertOnSubmit(tmp);
                            db.SubmitChanges();
                        }
                    }
                    var props = db.Properties;
                    return View(props);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/DeleteProperty/1
        public ActionResult DeleteProperty(int? id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id != null)
                    {
                        var tmpProp = db.Properties.Where(p => p.PROPERTY_ID == id).FirstOrDefault();

                        if (tmpProp != null)
                        {
                            var tmpConn = db.Connectors.Where(c => c.PropValue.PROPERTY_ID == tmpProp.PROPERTY_ID);
                            if(tmpConn != null) { 
                                db.Connectors.DeleteAllOnSubmit(tmpConn);
                            }

                            var tmpVal = db.PropValues.Where(p => p.PROPERTY_ID == tmpProp.PROPERTY_ID);
                            if (tmpVal != null) {
                                db.PropValues.DeleteAllOnSubmit(tmpVal);
                            }
                            db.Properties.DeleteOnSubmit(tmpProp);
                            db.SubmitChanges();
                        }
                    }
                    return RedirectToAction("ListOfProperties", "Admins");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/EditProperty/1
        public ActionResult EditProperty(int? id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id != null)
                    {
                        var prop = db.Properties.Where(p => p.PROPERTY_ID == id).FirstOrDefault();

                        if (prop != null)
                            return View(prop);

                        return RedirectToAction("ListOfProperties");
                    }
                    return RedirectToAction("ListOfProperties");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditProperty(Property propEdited)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (propEdited != null)
                    {
                        var prop = db.Properties.Where(p => p.PROPERTY_ID == propEdited.PROPERTY_ID).FirstOrDefault();

                        if (prop != null) {
                            prop.NAME = propEdited.NAME;
                            db.SubmitChanges();
                        }

                        return RedirectToAction("ListOfProperties");
                    }
                    return RedirectToAction("ListOfProperties");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/ValuesOfProperty/1
        public ActionResult ValuesOfProperty(int id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    var values = db.PropValues.Where(i => i.PROPERTY_ID == id);
                    return View(values);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ValuesOfProperty(string newVal, int? id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (newVal != null)
                    {
                        var check = db.PropValues.Where(u => u.VALUE == newVal).FirstOrDefault();

                        if (check == null && id != null)
                        {
                            var tmp = new PropValue();
                            tmp.VALUE = newVal;
                            tmp.PROPERTY_ID = (int)id;

                            db.PropValues.InsertOnSubmit(tmp);
                            db.SubmitChanges();
                        }
                    }
                    var values = db.PropValues.Where(i => i.PROPERTY_ID == id);
                    return View(values);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/EditValueOfProperty/1
        public ActionResult EditValueOfProperty(int? id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id != null)
                    {
                        var value = db.PropValues.Where(p => p.VALUE_ID == id).FirstOrDefault();

                        if (value != null)
                            return View(value);
                    }
                    return RedirectToAction("ListOfProperties");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditValueOfProperty(PropValue valueEdited)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (valueEdited != null)
                    {
                        var val = db.PropValues.Where(p => p.VALUE_ID == valueEdited.VALUE_ID).FirstOrDefault();

                        if (val != null)
                        {
                            val.VALUE = valueEdited.VALUE;
                            db.SubmitChanges();
                        }
                    }
                    return RedirectToAction("ListOfProperties");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/DeleteProperty/1
        public ActionResult DeleteValueOfProperty(int? id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    if (id != null)
                    {
                        var tmpVal = db.PropValues.Where(p => p.VALUE_ID == id).FirstOrDefault();

                        if (tmpVal != null)
                        {
                            var tmpConn = db.Connectors.Where(c => c.PropValue.VALUE_ID == tmpVal.VALUE_ID);
                            if (tmpConn != null)
                            {
                                db.Connectors.DeleteAllOnSubmit(tmpConn);
                            }

                            db.PropValues.DeleteOnSubmit(tmpVal);
                            db.SubmitChanges();
                        }
                    }
                    return RedirectToAction("ListOfProperties", "Admins");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins/PropertiesOfItem/1
        [HttpGet]
        public ActionResult PropertiesOfItem(int id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    var currentProps = db.Connectors.Where(c => c.ITEM_ID == id);
                    if (db.Connectors.Where(c => c.ITEM_ID == id).Any())
                    {
                        ViewBag.curProps = currentProps;
                    }
                    else
                    {
                        ViewBag.curProps = null;
                    }
                    var model = GetFullAndPartialViewPropertiesModel();
                    model.item = db.Items.Where(i => i.ITEM_ID == id).FirstOrDefault();
                    model.connectors = db.Connectors.Where(c => c.ITEM_ID == id).OrderBy(c => c.PropValue.Property.NAME);

                    return this.View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Admins/PropertiesOfItem/1
        [HttpPost]
        public ActionResult PropertiesOfItem(int VAL_ID,int ITEM_ID)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    var value = db.PropValues.Where(p => p.VALUE_ID == VAL_ID).FirstOrDefault();
                    var checker = db.Connectors.Where(c => c.ITEM_ID == ITEM_ID && c.PropValue.PROPERTY_ID == value.PROPERTY_ID).FirstOrDefault();
                    if(checker == null)
                    {
                        var newConnector = new Connector();
                        newConnector.ITEM_ID = ITEM_ID;
                        newConnector.VALUE_ID = VAL_ID;
                        db.Connectors.InsertOnSubmit(newConnector);
                        db.SubmitChanges();
                    }
                    else
                    {
                        checker.VALUE_ID = VAL_ID;

                        db.SubmitChanges();
                    }
                    return RedirectToAction("PropertiesOfItem","Admins", ITEM_ID);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteConnector(int VAL_ID, int ITEM_ID)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.admin))
                {
                    var checker = db.Connectors.Where(c => c.ITEM_ID == ITEM_ID && c.PropValue.VALUE_ID == VAL_ID).FirstOrDefault();
                    if(checker != null)
                    {
                        db.Connectors.DeleteOnSubmit(checker);
                        db.SubmitChanges();
                    }

                    return RedirectToAction("PropertiesOfItem", "Admins",new { id = ITEM_ID });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetPropValues(int propertyId)
        {

            var model = GetFullAndPartialViewPropertiesModel(propertyId);
            return PartialView("_PropertiesOfItem", model.pValues);
        }

        [HttpGet]
        private ItemContainer GetFullAndPartialViewPropertiesModel(int propertyId = 0)
        {
            var fullAndPartialViewModel = new ItemContainer();
            fullAndPartialViewModel.properties = db.Properties;
            fullAndPartialViewModel.pValues = db.PropValues.Where(p => p.PROPERTY_ID == propertyId);

            return fullAndPartialViewModel;
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