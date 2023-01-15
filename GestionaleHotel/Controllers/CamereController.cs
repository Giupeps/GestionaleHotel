using GestionaleHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Controllers
{
    public class CamereController : Controller
    {
        // GET: Camere
        public ActionResult PWIndex()
        {
            return PartialView("_PWIndex", Camera.GetCamere());
        }

        // GET: Camere/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Camere/Create
        public ActionResult Create()
        {
            ViewBag.Dropdowncamere = Camera.GetTipologie();
            return View();
        }

        // POST: Camere/Create
        [HttpPost]
        public ActionResult Create(Camera ca)
        {
            try
            {
                // TODO: Add insert logic here
                Camera.CreateCamera(ca);
                return RedirectToAction("MostraRiepilogo", "Riepilogo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Camere/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Dropdowncamere = Camera.GetTipologie();
            return View(Camera.GetSingolCamera(id));
        }

        // POST: Camere/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Camera ca)
        {
            try
            {
                // TODO: Add update logic here
                Camera.ModificaCamera(ca, id);
                return RedirectToAction("MostraRiepilogo", "Riepilogo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Camere/Delete/5
        public ActionResult Delete(int id)
        {
            Camera.DeleteCamera(id);
            return RedirectToAction("MostraRiepilogo", "Riepilogo");
        }

    }
}
