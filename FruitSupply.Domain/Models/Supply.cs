using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class Supply : NativeEntity
    {
        public Supplier Supplier { get; set; }
        public DateTime DeliveryDate { get; set; }

        public List<SupplyDetail> SupplyDetails { get; set; }
    }
}
