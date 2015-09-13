using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CarWorkshop.DataAccess.Entities.Base;
using Newtonsoft.Json;

namespace CarWorkshop.DataAccess.Entities
{
    public class SparePartJob
    {
        public int Id { get; set; }

        public Car Car { get; set; }
        public List<SparePart> SpareParts { get; set; }
        
        public decimal Price { get; set; }

        [NotMapped]
        public string SparePartsTags { get; set; }
    }

    public class SparePart : Lookup
    {
        [JsonIgnore]
        public List<SparePartJob> SparePartJobs { get; set; }
    }
}