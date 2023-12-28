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
    public class SupplyDetailRepository : IRepository<SupplyDetail>
    {
        private ApplicationDbContext _context;

        public SupplyDetailRepository()
        {
            _context = ApplicationDbContext.Instance;
        }

        public void Create(SupplyDetail item)
        {
            _context.SupplyDetail.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.SupplyDetail.Find(id);
            _context.SupplyDetail.Remove(obj);
            _context.SaveChanges();
        }

        public SupplyDetail Get(int id)
        {
            var obj = _context.SupplyDetail.Find(id);
            if (obj == null) return null;
            else return obj;
        }

        public List<SupplyDetail> GetList() => _context.SupplyDetail.ToList();

        public void Update(SupplyDetail item)
        {
            var obj = _context.SupplyDetail.Find(item.Id);

            obj.Supply = item.Supply;
            obj.Product = item.Product;
            obj.Unit = item.Unit;
            obj.UnitCount = item.UnitCount;
            obj.TotalCost = item.TotalCost;
            

            _context.SupplyDetail.Update(obj);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
