using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class Product
    {
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
    }
}
