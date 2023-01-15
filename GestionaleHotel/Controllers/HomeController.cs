using GestionaleHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionaleHotel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Exclude ="Ruolo")] Users user)
        {
            if (Users.AuthenticateUser(user.Username, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                ViewBag.MessaggioErrore = "Credenziali errate";
                return View();
            }
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }


}
}