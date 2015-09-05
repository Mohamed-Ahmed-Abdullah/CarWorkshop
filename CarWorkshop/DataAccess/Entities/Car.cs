using CarWorkshop.DataAccess.Entities.Base;

namespace CarWorkshop.DataAccess.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public CarType CarType { get; set; }
        public CarModel CarModel { get; set; }
    }

    public class CarType : Lookup {}
    public class CarModel : Lookup {}

}