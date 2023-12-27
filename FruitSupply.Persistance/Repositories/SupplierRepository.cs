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
    internal class SupplierRepository : IRepository<Supplier>
    {
        private ApplicationDbContext _context;

        public SupplierRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(Supplier item)
        {
            _context.Supplier.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Supplier.Find(id);
            _context.Supplier.Remove(obj);
            _context.SaveChanges();
        }

        public Supplier Get(int id)
        {
            var obj = _context.Supplier.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public IEnumerable<Supplier> GetList() => _context.Supplier.ToList();

        public void Update(Supplier item)
        {
            var obj = _context.Supplier.Find(item.Id);

            obj.Name = item.Name;
            obj.ContactInfo = item.ContactInfo;

            _context.Supplier.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
