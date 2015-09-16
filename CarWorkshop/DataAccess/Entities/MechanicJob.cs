using System;
using System.Collections.Generic;
using CarWorkshop.DataAccess.Entities.Base;
using Newtonsoft.Json;

namespace CarWorkshop.DataAccess.Entities
{
    public class MechanicJob
    {
        public int Id { get; set; }        
        public DateTime DateTime { get; set; } = DateTime.Now;

        public Car Car { get; set; }
        public MechanicJobType MechanicType { get; set; }

        public decimal Price { get; set; }
    }

    public class MechanicJobType : Lookup
    {
        [JsonIgnore]
        public List<MechanicJob> MechanicJobs { get; set; }
    }
}