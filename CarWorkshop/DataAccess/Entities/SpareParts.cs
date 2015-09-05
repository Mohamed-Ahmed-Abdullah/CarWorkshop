using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarWorkshop.DataAccess.Entities.Base;

namespace CarWorkshop.DataAccess.Entities
{
    public class SparePartJob
    {
        public int Id { get; set; }

        public Car Car { get; set; }
        public SparePart SparePart { get; set; }
        
        public decimal Price { get; set; }

    }

    public class SparePart : Lookup
    {
    }
}