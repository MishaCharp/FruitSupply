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
    public class ProductTypeRepository : IRepository<ProductType>
    {
        private ApplicationDbContext _context;

        public ProductTypeRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(ProductType item)
        {
            _context.ProductType.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.ProductType.Find(id);
            _context.ProductType.Remove(obj);
            _context.SaveChanges();
        }

        public ProductType Get(int id)
        {
            var obj = _context.ProductType.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public IEnumerable<ProductType> GetList() => _context.ProductType.ToList();

        public void Update(ProductType item)
        {
            var obj = _context.ProductType.Find(item.Id);

            obj.Type = item.Type;

            _context.ProductType.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
