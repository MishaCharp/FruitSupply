using FruitSupply.Domain.Models;
using FruitSupply.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Persistence.Repositories.Base;

namespace FruitSupply.Persistance.Repositories
{
    public class SupplierPriceRepository : IRepository<SupplierPrice>
    {
        private ApplicationDbContext _context;

        public SupplierPriceRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(SupplierPrice item)
        {
            _context.SupplierPrice.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.SupplierPrice.Find(id);
            _context.SupplierPrice.Remove(obj);
            _context.SaveChanges();
        }

        public SupplierPrice Get(int id)
        {
            var obj = _context.SupplierPrice.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public List<SupplierPrice> GetList() => _context.SupplierPrice.ToList();

        public void Update(SupplierPrice item)
        {
            var obj = _context.SupplierPrice.Find(item.Id);

            obj.Supplier = item.Supplier;
            obj.Product = item.Product;
            obj.UnitPrice = item.UnitPrice;
            obj.Unit = item.Unit;
            obj.StartPeriod = item.StartPeriod;
            obj.EndPeriod = item.EndPeriod;

            _context.SupplierPrice.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
