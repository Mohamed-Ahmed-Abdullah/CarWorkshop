using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWorkshop.Controllers
{
    public class PaintingJobController : Controller
    {
        // GET: PaintingJob
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaintingJob/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaintingJob/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaintingJob/Create
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

        // GET: PaintingJob/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaintingJob/Edit/5
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

        // GET: PaintingJob/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaintingJob/Delete/5
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
