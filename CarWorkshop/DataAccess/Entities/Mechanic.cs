using CarWorkshop.DataAccess.Entities.Base;

namespace CarWorkshop.DataAccess.Entities
{
    public class MechanicJob
    {
        public int Id { get; set; }

        public Car Car { get; set; }
        public MechanicJobType MechanicType { get; set; }

        public decimal Price { get; set; }

    }

    public class MechanicJobType : Lookup { }
}