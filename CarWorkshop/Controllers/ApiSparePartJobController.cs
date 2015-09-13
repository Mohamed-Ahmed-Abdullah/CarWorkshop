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
        public List<CarType> GetCarTypes()
        {
            return new DatabaseContext().CarTypes.ToList();
        }

        public void AddCarType(string carType)
        {
            if (string.IsNullOrWhiteSpace(carType))
                throw new Exception("please privide a cartype");

            var db = new DatabaseContext();
            var dbCarType = db.CarTypes.ToList().FirstOrDefault(f => f.ArabicName.Trim() == carType.Trim());
            if (dbCarType != null)
                return ;

            db.CarTypes.Add(new CarType {ArabicName = carType});
            db.SaveChanges();
        }

        public List<CarModel> GetCarModels()
        {
            return new DatabaseContext().CarModels.ToList();
        }

        public void AddCarModel(string carModel)
        {
            if (string.IsNullOrWhiteSpace(carModel))
                throw new Exception("please privide a cartype");

            var db = new DatabaseContext();
            var dbCarModel = db.CarModels.ToList().FirstOrDefault(f => f.ArabicName.Trim() == carModel.Trim());
            if (dbCarModel != null)
                return;

            db.CarModels.Add(new CarModel { ArabicName = carModel });
            db.SaveChanges();
        }

        public int GetPagesCount(int itemsPerPage)
        {
            if (itemsPerPage <= 0 )
                return 0;

            return (int) Math.Ceiling(((double) new DatabaseContext().SparePartJobs.Count())/itemsPerPage);
        }

        public List<string> GetSpareParts()
        {
            return new DatabaseContext().SpareParts.Select(s => s.ArabicName).ToList();
        }

        public List<SparePartJob> Get(int itemsPerPage, int currentPage)
        {
            var entities = new DatabaseContext().SparePartJobs
                .Include("Car")
                .Include("Car.CarModel")
                .Include("Car.CarType")
                .Include("SpareParts")
                .OrderBy(o=>o.Id)
                .Skip(itemsPerPage * (currentPage - 1))
                .Take(itemsPerPage)
                .ToList();

            var sparePartJobs = entities.Select(s => new SparePartJob
            {
                Id = s.Id,
                Price = s.Price ,
                Car = new Car
                {
                    Id = s.Car.Id,
                    CarNumber = s.Car.CarNumber,
                    CarType = new CarType {  Id = s.Car.CarType.Id , ArabicName = s.Car.CarType.ArabicName},
                    CarModel = new CarModel { Id = s.Car.CarModel.Id , ArabicName = s.Car.CarModel.ArabicName},
                },
                SpareParts = s.SpareParts.Select(sp=>new SparePart { Id = sp.Id , ArabicName = sp.ArabicName}).ToList(),
            })
            .ToList();

            return sparePartJobs;
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
                var car = db.Cars.FirstOrDefault(f => f.CarNumber == sparePartJob.Car.CarNumber) ?? sparePartJob.Car;

                var carType = db.CarTypes.FirstOrDefault(f => f.Id == sparePartJob.Car.CarType.Id);
                car.CarType = carType ?? sparePartJob.Car.CarType;

                var carModel = db.CarModels.FirstOrDefault(f => f.Id == sparePartJob.Car.CarModel.Id);
                car.CarModel = carModel ?? sparePartJob.Car.CarModel;

                sparePartJob.Car = car;
                sparePartJob.SpareParts = GetSpareParts(db,sparePartJob.SparePartsTags);

                db.SparePartJobs.Add(sparePartJob);
            }
            else
            {
                var conceptToUpdate = db.SparePartJobs
                    .Include("Car")
                    .Include("SpareParts")
                    .FirstOrDefault(s => s.Id == sparePartJob.Id);
                if (conceptToUpdate == null)
                    throw new Exception("Entity you want to update is not exists");

                if (conceptToUpdate.Car == null)
                    conceptToUpdate.Car = new Car();

                //Update
                conceptToUpdate.Car.CarNumber = sparePartJob.Car.CarNumber;

                var carType = db.CarTypes.FirstOrDefault(f => f.Id == sparePartJob.Car.CarType.Id);
                conceptToUpdate.Car.CarType = carType ?? sparePartJob.Car.CarType;

                var carModel = db.CarModels.FirstOrDefault(f => f.Id == sparePartJob.Car.CarModel.Id);
                conceptToUpdate.Car.CarModel = carModel ?? sparePartJob.Car.CarModel;

                conceptToUpdate.Price = sparePartJob.Price;

                if (!string.IsNullOrWhiteSpace(sparePartJob.SparePartsTags))
                {
                    var all = GetSpareParts(db, sparePartJob.SparePartsTags);
                    var newToAdd = all.Except(conceptToUpdate.SpareParts).ToList();
                    var toRemove = conceptToUpdate.SpareParts.Except(all).ToList();
                    //added
                    conceptToUpdate.SpareParts.AddRange(newToAdd);
                    //removed
                    toRemove.ForEach(f=> conceptToUpdate.SpareParts.Remove(f));
                }
            }

            db.SaveChanges();
        }

        private List<SparePart> GetSpareParts(DatabaseContext db,string tags)
        {
            if (string.IsNullOrWhiteSpace(tags))
                return null;

            var allSpareParts = db.SpareParts.ToList();

            var sparePartTags = tags.Split(',').ToList();
            var existingSpareParts = allSpareParts.Where(w => sparePartTags.Contains(w.ArabicName)).ToList();
            var existingSparePartsNames = existingSpareParts.Select(s => s.ArabicName).ToList();

            //if there is no tag like that add it
            var notExistingSparePartsNames = sparePartTags.Where(w => !existingSparePartsNames.Contains(w)).ToList();
            var notExistingSpareParts = notExistingSparePartsNames.Select(s => new SparePart { ArabicName = s }).ToList();

            db.SpareParts.AddRange(notExistingSpareParts);
            //db.SaveChanges();

            return existingSpareParts.Union(notExistingSpareParts).ToList();
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new Exception("please privide id");

            var db = new DatabaseContext();
            var sparePartJob = db.SparePartJobs.FirstOrDefault(f => f.Id == id);

            if (sparePartJob == null)
                throw new Exception("you are tring to delete an item that is already deleted");

            db.SparePartJobs.Remove(sparePartJob);
            db.SaveChanges();
        }
    }
}