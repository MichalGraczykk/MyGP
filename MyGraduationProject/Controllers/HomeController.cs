using DatabaseAccess;
using MyGraduationProject.Models;
using PagedList;
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

        public ActionResult Assortment(string sorting, string filtrationNAME, int? page, DateTime? dateFrom, DateTime? dateTo)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
                ViewBag.Auth = (User)Session["principal"];

            ViewBag.SortedBy = sorting;
            ViewBag.SortByNAME = sorting == null ? "NAME_Malejaco" : "";
            ViewBag.SortByPRICE_PER_DAY = sorting == "PRICE_PER_DAY_Malejaco" ? "PRICE_PER_DAY_Rosnaco" : "PRICE_PER_DAY_Malejaco";
            ViewBag.dateFrom = null;
            ViewBag.dateTo = null;
            ViewBag.FindNAME = null;

            var items = repo.GetAvailableItems();

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            if (ModelState.IsValid)
            {
                if(dateFrom != null && dateTo != null)
                {
                    items = repo.GetListOfAvailableItems((DateTime)(dateFrom), (DateTime)(dateTo));
                    ViewBag.dateFrom = dateFrom;
                    ViewBag.dateTo = dateTo;
                }                

                if (filtrationNAME != null && filtrationNAME != "")
                {
                    //TODO sprawdz czemu nie dziala
                    items = items.Where(i => i.NAME.Contains(filtrationNAME));
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

                
                return View(items.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(items.ToPagedList(pageNumber, pageSize));
            }
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
                var user = repo.GetUserByLoginAndPass(model.LOGIN, model.PASSWORD);

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

            //TODO test github

            //ferrari enzo id 2
            DateTime currDate = DateTime.Now;
            var items = db.Reservations.Where(i => i.ITEM_ID == id && i.DATE_TO > currDate);
            List<ToCallendar> tmp = new List<ToCallendar>();
            TimeSpan correct = new TimeSpan(1, 0, 0, 0);
            foreach (Reservation res in items)
            {
                tmp.Add(new ToCallendar() { title = "reserved", start = res.DATE_FROM.Value.Add(correct), end = res.DATE_TO.Value.Add(correct) });
            }
            ViewBag.MyEventList = tmp;

            var item = repo.GetItemById(id);

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