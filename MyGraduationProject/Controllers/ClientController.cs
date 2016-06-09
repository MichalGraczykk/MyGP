using DatabaseAccess;
using MyGraduationProject.Models;
using MyGraduationProject.Models.Enums;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyGraduationProject.Controllers
{
    public class ClientController : Controller
    {
        private DataClassesDataContext db = new DataClassesDataContext();

        private MyRepository repo = new MyRepository();

        // GET: Client
        public ActionResult Index()
        {
            //TODO zrobic edycje profilu
            return View();
        }

        public ActionResult ListOfArticles(string sorting, string filtrationNAME, int? page, DateTime? dateFrom, DateTime? dateTo)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.client))
                {
                    ViewBag.SortedBy = sorting;
                    ViewBag.SortByNAME = sorting == null ? "NAME_Malejaco" : "";
                    ViewBag.SortByPRICE_PER_DAY = sorting == "PRICE_PER_DAY_Malejaco" ? "PRICE_PER_DAY_Rosnaco" : "PRICE_PER_DAY_Malejaco";
                    ViewBag.dateFrom = null;
                    ViewBag.dateTo = null;
                    var items = repo.GetAvailableItems();

                    if (ModelState.IsValid)
                    {
                        if (dateFrom != null && dateTo != null)
                        {
                            items = repo.GetListOfAvailableItems((DateTime)(dateFrom), (DateTime)(dateTo));
                            ViewBag.dateFrom = dateFrom;
                            ViewBag.dateTo = dateTo;
                        }

                        if (filtrationNAME != null && filtrationNAME != "")
                        {
                            //TODO sprawdz czemu nie dziala
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

                        int pageSize = 2;
                        int pageNumber = (page ?? 1);
                        return View(items.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return View(items);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmOrder(int? id, DateTime? dateFrom, DateTime? dateTo)
        {
            ViewBag.Auth = null;
            if (Session["principal"] != null)
            {
                ViewBag.Auth = (User)(Session["principal"]);
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.client))
                {
                    DateTime currentDate = DateTime.Now;
                    if (id != null && dateFrom != null && dateTo != null && dateFrom < dateTo && dateFrom > currentDate)
                    {
                        var item = repo.GetItemById((int)id);

                        Reservation newReservation = new Reservation();

                        DateTime dateF = (DateTime)dateFrom;
                        dateF = dateF.Date;
                        DateTime dateT = (DateTime)dateTo;
                        dateT = dateT.Date;

                        newReservation.DATE_FROM = dateF;
                        newReservation.DATE_TO = dateT;
                        newReservation.ORDER_DATE = currentDate.Date;
                        newReservation.ITEM_ID = id;
                        newReservation.OVERALL_PRICE = (item.PRICE_PER_DAY)*((dateF - dateT).Days);

                        return View(newReservation);
                    }
                    return RedirectToAction("ListOfArticles", "Client");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ListOfArticles(Reservation reservation)
        {
            if (Session["principal"] != null)
            {
                var current = new User();
                current = (User)(Session["principal"]);

                if (current.ROLE_ID == (int)(RolesEnum.client))
                {
                    DateTime currentDate = DateTime.Now;
                    if (reservation.ITEM_ID != null && reservation.DATE_FROM != null && reservation.DATE_TO != null && reservation.DATE_FROM < reservation.DATE_TO && reservation.DATE_FROM > currentDate)
                    {
                        var item = repo.GetItemById((int)reservation.ITEM_ID);

                        Reservation newReservation = new Reservation();

                        DateTime dateF = (DateTime)reservation.DATE_FROM;
                        dateF = dateF.Date;
                        DateTime dateT = (DateTime)reservation.DATE_TO;
                        dateT = dateT.Date;

                        //sprawdzamy czy produkt na pewno jest dostępny
                        //mogło dojść do sytuacji w której dwóch użytkowników zamawiało ten sam produkt w podobnym czasie i na podobny okres(rezerwacji dokona osoba która pierwsza potwierdzi jej dane)
                        if (repo.isItemAvailable((int)reservation.ITEM_ID, dateF, dateT))
                        {
                            newReservation.DATE_FROM = dateF;
                            newReservation.DATE_TO = dateT;
                            newReservation.ORDER_DATE = currentDate.Date;
                            newReservation.ITEM_ID = reservation.ITEM_ID;
                            newReservation.USER_ID = current.USER_ID;
                            newReservation.OVERALL_PRICE = (item.PRICE_PER_DAY) * ((dateF - dateT).Days);

                            db.Reservations.InsertOnSubmit(newReservation);
                            db.SubmitChanges();

                            //TODO zwracamy odwolanie do kontrolera z lista zamowien uzytkownika
                            return RedirectToAction("Index", "Home");
                        }
                        //item przestał byc dostępny więc zwracamy widok z listą produktów
                        return RedirectToAction("ListOfArticles", "Client");
                    }
                    else
                    {
                        //dane wejściowe były złe więc zwracamy widok z listą produktów
                        return RedirectToAction("ListOfArticles", "Client");
                    }
                }
                //osoba nieposiadająca uprawnień próbuje stworzyć zamówienie więc zwracamy jej widok strony startowej
                return RedirectToAction("Index", "Home");
            }
            //osoba niezalogowana próbuje stworzyć zamówienie więc zwracamy jej widok strony startowej
            return RedirectToAction("Index", "Home");
        }

    }
}