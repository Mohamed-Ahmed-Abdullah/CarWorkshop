using CarWorkshop.DataAccess.Entities.Base;

namespace CarWorkshop.DataAccess.Entities
{
    public class PaintingJob
    {
        public int Id { get; set; }

        public Car Car { get; set; }
        public PaintingJobType PaintJobType { get; set; }

        public decimal Price { get; set; }
    }

    public class PaintingJobType : Lookup { }
}