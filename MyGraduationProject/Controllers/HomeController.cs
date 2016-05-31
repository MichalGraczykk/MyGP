using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyGraduationProject.Controllers
{
    public class HomeController : Controller
    {
        private DataClassesDataContext db = new DataClassesDataContext();
        // GET: Home
        public ActionResult Index()
        {
            
            ViewData["tytul"] = "TytulTest";
            ViewData["liczba"] = "LiczbaTest";


                return View(db.Users.ToList());
        }

        // GET: /Users/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        //przesyłane wraz z POST, zabezpiecza przed złośliwą podmianą danych
        [ValidateAntiForgeryToken]
        //tutaj sprzężamy nasze pola z formularza z polami z modelu
        public ActionResult Create([Bind(Include="LOGIN,PASSWORD,NAME,SURNAME,ROLE_ID")] User user)
        {
            //pamietaj o adresie(ADRESS_ID)

            //sprawdzamy czy wystąpił jakiś błąd, np. błędny typ danych w formualrzu
            if (ModelState.IsValid)
            {
                //dodanie samochodu
                //db.Users.Add(user);
                //zapsiane zmian
                //db.SaveChanges();
                //przekierowanie do strony o akcji Index
                return RedirectToAction("Index");
            }
            //jeśli ModelState.IsValid wracamy z powrotem do formularza
            return View();
        }
    }
}