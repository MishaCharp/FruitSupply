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
    public class UnitRepository : IRepository<Unit>
    {
        private ApplicationDbContext _context;

        public UnitRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(Unit item)
        {
            _context.Unit.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Unit.Find(id);
            _context.Unit.Remove(obj);
            _context.SaveChanges();
        }

        public Unit Get(int id)
        {
            var obj = _context.Unit.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public List<Unit> GetList() => _context.Unit.ToList();

        public void Update(Unit item)
        {
            var obj = _context.Unit.Find(item.Id);

            obj.Name = item.Name;

            _context.Unit.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
