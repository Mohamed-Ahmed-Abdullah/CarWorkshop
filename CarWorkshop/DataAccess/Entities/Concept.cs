namespace CarWorkshop.DataAccess.Entities
{
    public class Concept
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public byte[] Image { get; set; }
    }
}