using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWorkshop.DataAccess;

namespace CarWorkshop.Controllers
{
    public class ConceptController : Controller
    {
        // GET: Concept
        public ActionResult List()
        {
            return View(new DatabaseContext().Concepts.ToList());
        }

        // GET: Concept/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Concept/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Concept/Create
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

        // GET: Concept/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Concept/Edit/5
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

        // GET: Concept/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Concept/Delete/5
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
