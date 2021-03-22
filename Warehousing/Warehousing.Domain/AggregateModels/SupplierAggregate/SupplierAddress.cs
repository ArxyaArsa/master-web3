using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.SupplierAggregate
{
    public class SupplierAddress : ValueObject
    {
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return State;
            yield return Country;
            yield return PostalCode;
            yield return AddressLine1;
            yield return AddressLine2;
        }
    }
}
