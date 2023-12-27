using FruitSupply.Domain.Models;
using FruitSupply.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Persistence.Repositories.Base;

namespace FruitSupply.Persistance.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationDbContext _context;

        public ProductRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(Product item)
        {
            _context.Product.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Product.Find(id);
            _context.Product.Remove(obj);
            _context.SaveChanges();
        }

        public Product Get(int id)
        {
            var obj = _context.Product.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public IEnumerable<Product> GetList() => _context.Product.ToList();

        public void Update(Product item)
        {
            var obj = _context.Product.Find(item.Id);

            obj.Name = item.Name;
            obj.ProductType = item.ProductType;
            obj.ProductGrade = item.ProductGrade;

            _context.Product.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
