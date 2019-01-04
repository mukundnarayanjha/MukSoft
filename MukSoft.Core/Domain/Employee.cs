using System;

namespace MukSoft.Core.Domain
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public Address Address { get; set; }
    }
    public class Address : BaseEntity
    {
        public string City { get; set; }
        public string State { get; set; }

    }
}
