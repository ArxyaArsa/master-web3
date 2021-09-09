using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;

namespace Warehousing.Infrastructure.Repositories
{
    public class WarehouseLotRepository : IWarehouseLotRepository
    {
        private WarehousingContext _context;
        private DbSet<WarehouseLot> _dbSet;

        public WarehouseLotRepository(WarehousingContext context)
        {
            _context = context;
            _dbSet = context.WarehouseLots;
        }

        public WarehouseLot Add(WarehouseLot p)
        {
            var ret = _dbSet.Add(p);
            _context.SaveChanges();

            return ret.Entity;
        }

        public Task<WarehouseLot> GetAsync(int pId)
        {
            return _dbSet.FindAsync(pId);
        }

        public void Update(WarehouseLot p)
        {
            _context.SaveChanges(); // all actual changes will be done through the Domain entity methods
        }
    }
}
