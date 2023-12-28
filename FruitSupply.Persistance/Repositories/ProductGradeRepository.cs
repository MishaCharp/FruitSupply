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
    public class ProductGradeRepository : IRepository<ProductGrade>
    {
        private ApplicationDbContext _context;

        public ProductGradeRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(ProductGrade item)
        {
            _context.ProductGrade.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.ProductGrade.Find(id);
            _context.ProductGrade.Remove(obj);
            _context.SaveChanges();
        }

        public ProductGrade Get(int id)
        {
            var obj = _context.ProductGrade.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public List<ProductGrade> GetList() => _context.ProductGrade.ToList();

        public void Update(ProductGrade item)
        {
            var obj = _context.ProductGrade.Find(item.Id);

            obj.Grade = item.Grade;

            _context.ProductGrade.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
