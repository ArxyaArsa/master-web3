using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.SupplierAggregate
{
    public class Supplier : Entity, IAggregateRoot
    {
        //public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public SupplierContact Contact { get; private set; }
        public SupplierAddress Address { get; private set; }
        public DateTime AddDate { get; private set; }
        public DateTime? FirstContractDate { get; private set; }
        public DateTime? LastContractDate { get; private set; }


        private readonly List<Contract> _contracts;
        public IReadOnlyCollection<Contract> Contracts => _contracts;
    }
}
