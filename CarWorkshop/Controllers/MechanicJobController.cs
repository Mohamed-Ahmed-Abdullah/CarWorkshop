using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWorkshop.Controllers
{
    public class MechanicJobController : Controller
    {
        // GET: MechanicJob
        public ActionResult Index()
        {
            return View();
        }

        // GET: MechanicJob/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MechanicJob/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MechanicJob/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MechanicJob/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MechanicJob/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MechanicJob/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MechanicJob/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
