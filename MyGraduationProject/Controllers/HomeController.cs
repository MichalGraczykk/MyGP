using DatabaseAccess;
using MyGraduationProject.Models;
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
    }
}