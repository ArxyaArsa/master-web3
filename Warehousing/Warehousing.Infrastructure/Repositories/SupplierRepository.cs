using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Supplier> GetAsync(int sId, bool includeContracts = true)
        {
            var q = _dbSet.AsQueryable();

            if (includeContracts)
                q = q.Include(x => x.Contracts);

            return q.FirstOrDefaultAsync(x => x.Id == sId);
        }

        public Task<Contract> GetContractAsync(int cId, bool includeSupplier = true)
        {
            var q = _context.Contracts.AsQueryable();

            if (includeSupplier)
                q = q.Include(x => x.Supplier);

            return q.FirstOrDefaultAsync(x => x.Id == cId);
        }

        public void Update(Supplier p)
        {
            _context.SaveChanges(); // all actual changes will be done through the Domain entity methods
        }
    }
}
