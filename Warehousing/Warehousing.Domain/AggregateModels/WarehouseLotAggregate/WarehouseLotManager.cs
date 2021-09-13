using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
{
    public class WarehouseLotManager : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }

        public WarehouseLotManager() { }

        public WarehouseLotManager(string fName, string lName, string email, string phone) : this()
        {
            FirstName = fName ?? string.Empty;
            LastName = lName ?? string.Empty;
            Email = email ?? string.Empty;
            Phone = phone ?? string.Empty;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Phone;
            yield return Email;
        }
    }
}
