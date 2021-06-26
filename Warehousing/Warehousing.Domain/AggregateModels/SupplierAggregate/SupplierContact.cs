using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.SupplierAggregate
{
    public class SupplierContact : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Fax { get; private set; }

        public SupplierContact(string firstName, string lastName, string phone, string email, string fax)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Fax = fax;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Phone;
            yield return Email;
            yield return Fax;
        }
    }
}
