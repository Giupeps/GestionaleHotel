using GestionaleHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult PWIndex()
        {
            return PartialView("_PWIndex", Clienti.Getclienti());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Clienti c)
        {
            try
            {
                // TODO: Add insert logic here
                Clienti.Create(c);
                return RedirectToAction("MostraRiepilogo", "Riepilogo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Clienti.GetSingoloCliente(id));
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Clienti c)
        {
            try
            {
                // TODO: Add update logic here
                Clienti.ModificaCliente(id, c);
                return RedirectToAction("MostraRiepilogo", "Riepilogo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            Clienti.CancellaCliente(id);
            return RedirectToAction("MostraRiepilogo", "Riepilogo");
        }
    }
}
