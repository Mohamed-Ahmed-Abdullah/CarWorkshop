using System;
using CarWorkshop.DataAccess.Entities.Base;

namespace CarWorkshop.DataAccess.Entities
{
    public class DentingJob
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public Car Car { get; set; }
        public DentingJobType DentingType { get; set; }

        public decimal Price { get; set; }
    }

    public class DentingJobType : Lookup { }

}