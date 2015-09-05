using System.ComponentModel.DataAnnotations;

namespace CarWorkshop.DataAccess.Entities.Base
{
    public abstract class Lookup
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string ArabicName { get; set; }

        [StringLength(300)]
        public string EnglishName { get; set; }

        [StringLength(2000)]
        public string Notes { get; set; }
    }
}