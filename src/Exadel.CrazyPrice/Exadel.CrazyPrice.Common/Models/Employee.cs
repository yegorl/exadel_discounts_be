using System;

namespace Exadel.CrazyPrice.Common.Models
{
    public class Employee
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }

        public string PhoneNumber { get; init; }

        public string Mail { get; init; }
    }
}