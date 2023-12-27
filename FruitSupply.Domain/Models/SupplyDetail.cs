using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class SupplyDetail : NativeEntity
    {
        public Supply Supply { get; set; }
        public Product Product { get; set; }
        public Unit Unit { get; set; }
        public float UnitCount { get; set; }
        public decimal TotalCost { get; set; }
    }
}
