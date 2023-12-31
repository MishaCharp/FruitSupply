﻿using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class ProductType : NativeEntity
    {
        public string Type { get; set; }
        public List<Product> Products { get; set; }
    }
}
