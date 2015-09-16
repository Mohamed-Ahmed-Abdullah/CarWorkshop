using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CarWorkshop.DataAccess;
using CarWorkshop.DataAccess.Entities;

namespace CarWorkshop.Controllers
{
    public class ApiMechanicJobController : ApiController
    {
        public List<MechanicJobType> GetMechanicJobTypes()
        {
            return new DatabaseContext().MechanicJobTypes.ToList();
        }

        public void AddMechanicJobType(string mechanicJobType)
        {
            if (string.IsNullOrWhiteSpace(mechanicJobType))
                throw new Exception("please privide a mechanicJobType");

            var db = new DatabaseContext();
            var dbCarType = db.MechanicJobTypes.ToList().FirstOrDefault(f => f.ArabicName.Trim() == mechanicJobType.Trim());
            if (dbCarType != null)
                return;

            db.MechanicJobTypes.Add(new MechanicJobType { ArabicName = mechanicJobType });
            db.SaveChanges();
        }

        public int GetPagesCount(int itemsPerPage)
        {
            if (itemsPerPage <= 0)
                return 0;

            return (int)Math.Ceiling(((double)new DatabaseContext().MechanicJobs.Count()) / itemsPerPage);
        }

        public List<MechanicJob> Get(int itemsPerPage, int currentPage)
        {
            var entities = new DatabaseContext().MechanicJobs
                .Include("Car")
                .Include("Car.CarModel")
                .Include("Car.CarType")
                .Include("MechanicType")
                .OrderBy(o => o.Id)
                .Skip(itemsPerPage * (currentPage - 1))
                .Take(itemsPerPage)
                .ToList();

            var mechanicJobs = entities.Select(s => new MechanicJob
            {
                Id = s.Id,
                Price = s.Price,
                Car = new Car
                {
                    Id = s.Car.Id,
                    CarNumber = s.Car.CarNumber,
                    CarType = new CarType { Id = s.Car.CarType.Id, ArabicName = s.Car.CarType.ArabicName },
                    CarModel = new CarModel { Id = s.Car.CarModel.Id, ArabicName = s.Car.CarModel.ArabicName },
                },
                MechanicType = s.MechanicType,
            })
            .ToList();

            return mechanicJobs;
        }

    
        public void Post([FromBody]MechanicJob mechanicJob)
        {
            if (mechanicJob == null)
                throw new Exception("Entity Should not be null");

            var db = new DatabaseContext();

            //Save or Update?
            if (mechanicJob.Id == 0)
            {
                var car = db.Cars.FirstOrDefault(f => f.CarNumber == mechanicJob.Car.CarNumber) ?? mechanicJob.Car;

                var carType = db.CarTypes.FirstOrDefault(f => f.Id == mechanicJob.Car.CarType.Id);
                car.CarType = carType ?? mechanicJob.Car.CarType;

                var carModel = db.CarModels.FirstOrDefault(f => f.Id == mechanicJob.Car.CarModel.Id);
                car.CarModel = carModel ?? mechanicJob.Car.CarModel;

                mechanicJob.Car = car;

                var mechanicType = db.MechanicJobTypes.FirstOrDefault(f => f.Id == mechanicJob.MechanicType.Id);
                mechanicJob.MechanicType = mechanicType;

                db.MechanicJobs.Add(mechanicJob);
            }
            else
            {
                var entityToUpdate = db.MechanicJobs
                    .Include("Car")
                    .Include("MechanicType")
                    .FirstOrDefault(s => s.Id == mechanicJob.Id);
                if (entityToUpdate == null)
                    throw new Exception("Entity you want to update is not exists");

                if (entityToUpdate.Car == null)
                    entityToUpdate.Car = new Car();

                //Update
                entityToUpdate.Car.CarNumber = mechanicJob.Car.CarNumber;

                var carType = db.CarTypes.FirstOrDefault(f => f.Id == mechanicJob.Car.CarType.Id);
                entityToUpdate.Car.CarType = carType ?? mechanicJob.Car.CarType;

                var carModel = db.CarModels.FirstOrDefault(f => f.Id == mechanicJob.Car.CarModel.Id);
                entityToUpdate.Car.CarModel = carModel ?? mechanicJob.Car.CarModel;

                entityToUpdate.Price = mechanicJob.Price;

                var mechanicType = db.MechanicJobTypes.FirstOrDefault(f => f.Id == mechanicJob.MechanicType.Id);
                entityToUpdate.MechanicType = mechanicType;
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new Exception("please privide id");

            var db = new DatabaseContext();
            var entity = db.MechanicJobs.FirstOrDefault(f => f.Id == id);

            if (entity == null)
                throw new Exception("you are tring to delete an item that is already deleted");

            db.MechanicJobs.Remove(entity);
            db.SaveChanges();
        }
    }
}
