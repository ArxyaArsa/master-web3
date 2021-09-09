using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehousing.Domain.AggregateModels.SupplierAggregate;

namespace Warehousing.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private WarehousingContext _context;
        private DbSet<Supplier> _dbSet;

        public SupplierRepository(WarehousingContext context)
        {
            _context = context;
            _dbSet = context.Suppliers;
        }

        public Supplier Add(Supplier p)
        {
            var ret = _dbSet.Add(p);
            _context.SaveChanges();

            return ret.Entity;
        }

        public Task<Supplier> GetAsync(int pId)
        {
            return _dbSet.FindAsync(pId);
        }

        public void Update(Supplier p)
        {
            _context.SaveChanges(); // all actual changes will be done through the Domain entity methods
        }
    }
}
