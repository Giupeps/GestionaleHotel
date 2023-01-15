using GestionaleHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Controllers
{
    [Authorize]
    public class PrenotazioniController : Controller
    {
        // GET: Prenotazioni
        public ActionResult PWIndex()
        {
            return PartialView("_PWIndex", Prenotazione.GetPrenotazioni());
        }

        // GET: Prenotazioni/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Prenotazioni/Create
        public ActionResult Create()
        {
            ViewBag.ListaClientiDropdown = Clienti.GetClientiDropdown();
            ViewBag.DropdownNumeroCamere = Camera.GetCamereDropdown();
            ViewBag.PernottamentiDropdown = Pernottamento.GetPernottamentiDropdown();
            return View();
        }

        // POST: Prenotazioni/Create
        [HttpPost]
        public ActionResult Create(Prenotazione pr)
        {
            try
            {
                // TODO: Add insert logic here
                Prenotazione.CreatePrenotazione(pr);
                return RedirectToAction("MostraRiepilogo", "Riepilogo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prenotazioni/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.DropdownNumeroCamere = Camera.GetCamereDropdown();
            ViewBag.PernottamentiDropdown = Pernottamento.GetPernottamentiDropdown();
            return View(Prenotazione.GetPrenotazione(id));
        }

        // POST: Prenotazioni/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Prenotazione pr)
        {
            try
            {
                // TODO: Add update logic here
                Prenotazione.ModificaPrenotazione(pr, id);
                return RedirectToAction("MostraRiepilogo", "Riepilogo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prenotazioni/Delete/5
        public ActionResult Delete(int id)
        {
            Prenotazione.CancellaPrenotazione(id);
            return RedirectToAction("MostraRiepilogo", "Riepilogo");
        }

    } 
}
