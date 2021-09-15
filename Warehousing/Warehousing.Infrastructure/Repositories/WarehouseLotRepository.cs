using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<WarehouseLot> GetAsync(int pId, bool includeParcels = false)
        {
            var q = _dbSet.AsQueryable();

            if (includeParcels)
                q = q.Include(x => x.Parcels);

            return q.FirstOrDefaultAsync(x => x.Id == pId);
        }

        public Task<Parcel> GetParcelAsync(int pId, bool includeWarehouseLot = true, bool includeParcelType = true)
        {
            var q = _context.Parcels.AsQueryable();

            if (includeWarehouseLot)
                q = q.Include(x => x.WarehouseLot);

            if (includeParcelType)
                q = q.Include(x => x.ParcelType);

            return q.FirstOrDefaultAsync(x => x.Id == pId);
        }

        public Task<ParcelType> GetParcelTypeAsync(int ptId)
        {
            return _context.ParcelTypes.FirstOrDefaultAsync(x => x.Id == ptId);
        }

        public void Remove(WarehouseLot wl)
        {
            _dbSet.Remove(wl);
            _context.SaveChanges();
        }

        public void Update(WarehouseLot p)
        {
            _context.SaveChanges(); // all actual changes will be done through the Domain entity methods
        }
    }
}
