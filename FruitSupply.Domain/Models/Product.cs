﻿using FriutSupply.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.Domain.Models
{
    public class Product : NativeEntity
    {
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        public ProductGrade ProductGrade { get; set; }

        public List<SupplierPrice> SupplierPrices { get; set; }
        public List<SupplyDetail> SupplyDetails { get; set; }

    }
}
