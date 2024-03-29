﻿using System;
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

        public SupplierAddress() { }

        public SupplierAddress(string state, string country, string postalcode, string addressline1, string addressline2) : this()
        {
            State = state;
            Country = country;
            PostalCode = postalcode;
            AddressLine1 = addressline1;
            AddressLine2 = addressline2;
        }

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
