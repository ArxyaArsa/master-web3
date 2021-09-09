﻿using Microsoft.EntityFrameworkCore;
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
        private DbSet<Parcel> _dbSet;

        public ParcelRepository(WarehousingContext context)
        {
            _context = context;
            _dbSet = context.Parcels;
        }

        public Parcel Add(Parcel p)
        {
            var ret = _dbSet.Add(p);
            _context.SaveChanges();

            return ret.Entity;
        }

        public Task<Parcel> GetAsync(int pId)
        {
            return _dbSet.FindAsync(pId);
        }

        public void Update(Parcel p)
        {
            _context.SaveChanges(); // all actual changes will be done through the Domain entity methods
        }
    }
}
