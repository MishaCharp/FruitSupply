using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class Unit : NativeEntity
    {
        public string Name { get; set; }

        public List<SupplyDetail> SupplyDetails { get; set; }
        public List<SupplierPrice> SupplierPrices { get; set; }
    }
}
