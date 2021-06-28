using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehousing.Domain.AggregateModels.ParcelAggregate;

namespace Warehousing.Infrastructure.Repositories
{
    public class ParcelRepository : IParcelRepository
    {
        private WarehousingContext _context;

        public ParcelRepository(WarehousingContext context)
        {
            _context = context;
        }

        public Parcel Add(Parcel p)
        {
            var ret = _context.Parcels.Add(p);
            _context.SaveChanges();

            return ret.Entity;
        }

        public Task<Parcel> GetAsync(int pId)
        {
            return _context.Parcels.FindAsync(pId);
        }

        public void Update(Parcel p)
        {
            _context.SaveChanges();
        }
    }
}
