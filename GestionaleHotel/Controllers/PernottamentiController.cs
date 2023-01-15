using GestionaleHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Controllers
{
    public class PernottamentiController : Controller
    {
        // GET: Pernottamenti
        public ActionResult PWIndex()
        {
            return PartialView("_PWIndex", Pernottamento.GetPernottamento());
        }
    }
}