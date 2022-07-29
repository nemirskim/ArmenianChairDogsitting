using ArmenianChairDogsitting.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Entities
{
    public class PriceCatalog
    {
        public int Id { get; set; }
        public ServiceEnum Service { get; set; }
        public decimal Price { get; set; }
        public Sitter Sitter { get; set; }
    }
}
