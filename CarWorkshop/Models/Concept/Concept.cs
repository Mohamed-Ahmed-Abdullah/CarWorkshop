using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarWorkshop.Models.Concept
{
    public class Concept
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public byte[] Image { get; set; }
    }
}