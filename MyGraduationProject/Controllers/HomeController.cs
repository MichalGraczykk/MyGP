using DatabaseAccess;
using MyGraduationProject.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace MyGraduationProject.Controllers
{
    public class HomeController : Controller
    {
        //połączenie z bazą danych
        private DataClassesDataContext db = new DataClassesDataContext();

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
        {             //ten warunek sprawdza czy dane które pobieramy z widoku są we właściwej formie
            if (ModelState.IsValid)
            {
                //zapytanie które pobierze nam z bazy użytkownika o podanym loginie i haśle
                var user = db.Users.Where(u => u.LOGIN == model.LOGIN && u.PASSWORD == model.PASSWORD).FirstOrDefault();
                if (user != null)
                {
                    Session["principal"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //jeśli użytkownik poda zły login lub hasło to do widoku zostanie zwrócony odpowiedni komunikat
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["principal"] = null;
            return RedirectToAction("Index", "Home");
        }

        /*
       [HttpPost]
       [ValidateAntiForgeryToken]
       [AllowAnonymous]
       public ActionResult Login (LoginView model)
       {
           //ten warunek sprawdza czy dane które pobieramy z widoku są we właściwej formie
           if (ModelState.IsValid)
           {
               //zapytanie które pobierze nam z bazy użytkownika o podanym loginie i haśle
               var user = db.Users.Where(u => u.LOGIN == model.LOGIN && u.PASSWORD == model.PASSWORD).FirstOrDefault();
               if (user != null)
               {
                   /*tworzy ciasteczko które przetrzyma login, jeśli parametr rememberMe=true ciasteczko zostanie utworzone trwale,
                   to oznacza, że zostanie usunięte dopiero po wylogowaniu a nie np. przy zamknięciu przeglądarki*//*

        var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.NameIdentifier, user.LOGIN),
                        new Claim(ClaimTypes.Name, user.LOGIN),
                        new Claim("Role", user.Role.NAME)
                        }, DefaultAuthenticationTypes.ApplicationCookie);
 
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignIn(new AuthenticationProperties{ IsPersistent = model.RememberMe }, identity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //jeśli użytkownik poda zły login lub hasło to do widoku zostanie zwrócony odpowiedni komunikat
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(model);
        }

        
        public ActionResult Logout()
        {
            //ciasteczko sesji zostanie usunięte
           // var ctx = Request.GetOwinContext();
           // var authenticationManager = ctx.Authentication;
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

       

        // POST: /Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginView model)
        {
            //ten warunek sprawdza czy dane które pobieramy z widoku są we właściwej formie
            if (ModelState.IsValid)
            {
                //zapytanie które pobierze nam z bazy użytkownika o podanym loginie i haśle
                var user = db.Users.Where(u => u.LOGIN == model.LOGIN && u.PASSWORD == model.PASSWORD).FirstOrDefault();
                if(user != null)
                {
                    /*tworzy ciasteczko które przetrzyma login, jeśli parametr rememberMe=true ciasteczko zostanie utworzone trwale,
                    to oznacza, że zostanie usunięte dopiero po wylogowaniu a nie np. przy zamknięciu przeglądarki*//*
                    FormsAuthentication.SetAuthCookie(user.LOGIN, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //jeśli użytkownik poda zły login lub hasło to do widoku zostanie zwrócony odpowiedni komunikat
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            //ciasteczko sesji zostanie usunięte
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        */
    }
}