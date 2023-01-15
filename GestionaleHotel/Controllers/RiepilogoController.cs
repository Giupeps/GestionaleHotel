using GestionaleHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Controllers
{
    [Authorize]
    public class RiepilogoController : Controller
    {
        // GET: Riepilogo
        public ActionResult MostraRiepilogo()
        {
            return View();
        }

        public JsonResult GetPrenotazionibyCF(string CodiceFiscale)
        {
            List<Prenotazione> prenotazionebyCF = Prenotazione.GetPrenotazioniByCodFisc(CodiceFiscale);
            return Json(prenotazionebyCF, JsonRequestBehavior.AllowGet);
        }

    }
}