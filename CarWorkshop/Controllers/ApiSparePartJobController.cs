using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CarWorkshop.DataAccess;
using CarWorkshop.DataAccess.Entities;

namespace CarWorkshop.Controllers
{
    public class ApiSparePartJobController : ApiController
    {
        public List<SparePartJob> Get()
        {
            return new DatabaseContext().SparePartJobs
                .Include("Car")
                .Include("Car.CarModel")
                .Include("Car.CarType")
                .Include("SpareParts")
                .ToList();

        }

        public List<SparePart> GetSparePartLookup()
        {
            return new DatabaseContext().SpareParts.ToList();
        }

        public void Post([FromBody]SparePartJob sparePartJob)
        {
            if (sparePartJob == null)
                throw new Exception("Entity Should not be null");

            var db = new DatabaseContext();

            //Save or Update?
            if (sparePartJob.Id == 0)
            {
                db.SparePartJobs.Add(sparePartJob);
            }
            else
            {
                var conceptToUpdate = db.SparePartJobs.FirstOrDefault(s => s.Id == sparePartJob.Id);
                if (conceptToUpdate == null)
                    throw new Exception("Entity you want to update is not exists");

                //TODO:Update
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
        }
    }
}
