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
    public class SupplyRepository : IRepository<Supply>
    {
        private ApplicationDbContext _context;

        public SupplyRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(Supply item)
        {
            _context.Supply.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Supply.Find(id);
            _context.Supply.Remove(obj);
            _context.SaveChanges();
        }

        public Supply Get(int id)
        {
            var obj = _context.Supply.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public List<Supply> GetList() => _context.Supply.ToList();

        public void Update(Supply item)
        {
            var obj = _context.Supply.Find(item.Id);

            obj.Supplier = item.Supplier;
            obj.DeliveryDate = item.DeliveryDate;

            _context.Supply.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
