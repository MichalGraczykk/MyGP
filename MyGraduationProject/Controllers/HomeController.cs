using DatabaseAccess;
using MyGraduationProject.Models;
using MyGraduationProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyGraduationProject.Controllers
{
    public class HomeController : Controller
    {
        //połączenie z bazą danych
        private DataClassesDataContext db = new DataClassesDataContext();

        private MyRepository repo = new MyRepository();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            return View();
        }
        // GET/POST: Home/Assortment
        public ActionResult Assortment(string sorting, string filtrationNAME, DateTime? dateFrom = null, DateTime? dateTo = null,int? VAL_ID = null)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            ViewBag.SortedBy = sorting;
            ViewBag.SortByNAME = sorting == null ? "NAME_Malejaco" : "";
            ViewBag.SortByPRICE_PER_DAY = sorting == "PRICE_PER_DAY_Malejaco" ? "PRICE_PER_DAY_Rosnaco" : "PRICE_PER_DAY_Malejaco";
            ViewBag.dateF = null;
            ViewBag.dateT = null;
            ViewBag.FindNAME = null;
            ViewBag.VAL_ID = null;

            var model = new AssortmentContainer();
            model.properties = db.Properties;
            model.pValues = new List<PropValue> { new PropValue { VALUE_ID = 0, VALUE = "DEFAULT", PROPERTY_ID = 0 } };
            if (ModelState.IsValid)
            {
                var items = repo.GetAvailableItems();

                if (VAL_ID != null && VAL_ID != 0)
                {
                    var tmp = db.Connectors.Where(c => c.VALUE_ID == VAL_ID);
                    if (tmp == null)
                    {
                        model.items = items;
                        return View(model);
                    }
                    List<Item> tmpItems = new List<Item>();
                    foreach (var i in tmp)
                    {
                        tmpItems.Add(i.Item);
                    }
                    items = tmpItems;

                }
                ViewBag.VAL_ID = VAL_ID;

                if (dateFrom != null && dateTo != null)
                {
                    items = repo.GetListOfAvailableItems(items, (DateTime)(dateFrom), (DateTime)(dateTo));
                    ViewBag.dateF = dateFrom;
                    ViewBag.dateT = dateTo;
                }

                if (filtrationNAME != null && filtrationNAME != "")
                {
                    items = items.Where(i => i.NAME.ToUpper().Contains(filtrationNAME.ToUpper()));
                    ViewBag.FindNAME = filtrationNAME;
                }

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
                model.items = items;
                
                return View(model);
            }
            else
            {
                
                model.items = repo.GetAvailableItems();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult _PartialBrowser(int propertyId)
        {
            var tmpDEF = new List<PropValue> { new PropValue { VALUE_ID = 0, VALUE = "DEFAULT", PROPERTY_ID = 0 } };
            tmpDEF.AddRange(db.PropValues.Where(p => p.PROPERTY_ID == propertyId));
            return PartialView("_PartialBrowser", tmpDEF);
        }

        // GET: /Home/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView model)
        {   
            //ten warunek sprawdza czy dane które pobieramy z widoku są we właściwej formie
            if (ModelState.IsValid)
            {
                //zapytanie które pobierze nam z bazy użytkownika o podanym loginie i haśle
                //var user = repo.GetUserByLoginAndPass(model.LOGIN, model.PASSWORD);

                var user = db.Users.Where(u => u.LOGIN == model.LOGIN && u.PASSWORD == model.PASSWORD).FirstOrDefault();

                if (user != null)
                {
                    Session["principal"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //jeśli użytkownik poda zły login lub hasło to do widoku zostanie zwrócony odpowiedni komunikat
                    ModelState.AddModelError("", "zły login lub hasło");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["principal"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            return View();
        }

        public ActionResult CalendarOfItem(int id)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            DateTime currDate = DateTime.Now;
            var items = db.Reservations.Where(i => i.ITEM_ID == id && i.DATE_TO > currDate && i.STATUS_ID != (int)ReservationStatusesEnum.CANCELLED);
            List<ToCallendar> tmp = new List<ToCallendar>();
            TimeSpan correct = new TimeSpan(1, 0, 0, 0);
            foreach (Reservation res in items)
            {
                tmp.Add(new ToCallendar() { title = "reserved", start = res.DATE_FROM.Add(correct), end = res.DATE_TO.Add(correct) });
            }
            ViewBag.MyEventList = tmp;

            var item = repo.GetItemById(id);

            return View(item);
        }

        [HttpPost]
        public ActionResult CalendarOfItem(int? ITEM_ID, DateTime? tDateFrom, DateTime? tDateTo)
        {
            if(ITEM_ID == null)
            {
                return RedirectToAction("Index", "Home");
            }
            DateTime currentDate = DateTime.Now;
            if (ITEM_ID != null && tDateFrom != null && tDateTo != null && tDateFrom < tDateTo && tDateFrom > currentDate)
            {
                return RedirectToAction("ConfirmOrder", "Client", new { id = ITEM_ID, dateFrom = tDateFrom, dateTo = tDateTo});
            }

            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            var items = db.Reservations.Where(i => i.ITEM_ID == ITEM_ID && i.DATE_TO > currentDate);
            List<ToCallendar> tmp = new List<ToCallendar>();
            TimeSpan correct = new TimeSpan(1, 0, 0, 0);
            foreach (Reservation res in items)
            {
                tmp.Add(new ToCallendar() { title = "reserved", start = res.DATE_FROM.Add(correct), end = res.DATE_TO.Add(correct) });
            }
            ViewBag.MyEventList = tmp;

            var item = repo.GetItemById((int)ITEM_ID);

            return View(item);
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