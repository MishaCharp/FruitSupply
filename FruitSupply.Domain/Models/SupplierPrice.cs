using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class SupplierPrice : NativeEntity
    {
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public Unit Unit { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
    }
}
