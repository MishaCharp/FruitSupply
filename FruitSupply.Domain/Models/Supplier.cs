using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class Supplier : NativeEntity
    {
        public string Name { get; set; }
        public string? ContactInfo { get; set; }
    }
}
