using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
{
    public class WarehouseLot : Entity, IAggregateRoot
    {
        //public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }            // 'freezer', 'dry'
        public string Occupated { get; private set; }       // 'empty', 'has-items', 'full' 
        public decimal WeightCapacity { get; private set; }
        public DateTime? LastInventoryChange { get; private set; }
        public WarehouseLotManager Manager { get; private set; }
    }
}
