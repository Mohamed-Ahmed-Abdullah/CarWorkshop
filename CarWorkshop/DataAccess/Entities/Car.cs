using System;
using System.Collections.Generic;
using System.Data;
using CarWorkshop.DataAccess.Entities.Base;
using Newtonsoft.Json;

namespace CarWorkshop.DataAccess.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public DateTime CareationDate { get; set; } = DateTime.Now;

        public string CarNumber { get; set; }
        public CarType CarType { get; set; }
        public CarModel CarModel { get; set; }

        [JsonIgnore]
        public virtual List<SparePartJob> SparePartJobs { get; set; }
        [JsonIgnore]
        public virtual List<MechanicJob> MechanicJobs { get; set; }
        [JsonIgnore]
        public virtual List<PaintingJob> PaintingJobs { get; set; }
    }

    public class CarType : Lookup
    {
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; }
    }

    public class CarModel : Lookup
    {
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; }
    }
}