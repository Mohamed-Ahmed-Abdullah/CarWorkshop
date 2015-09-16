using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CarWorkshop.DataAccess;
using CarWorkshop.DataAccess.Entities;

namespace CarWorkshop.Controllers
{
    public class ApiCarController : ApiController
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
                return;

            db.CarTypes.Add(new CarType { ArabicName = carType });
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
    }
}