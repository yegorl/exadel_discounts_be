using Exadel.CrazyPrice.Common.Models.Option;
using System;

namespace Exadel.CrazyPrice.Common.Models
{
    public class User : IEquatable<User>
    {
        public virtual Guid Id { get; init; }

        public virtual string Name { get; init; }

        public virtual string Surname { get; init; }

        public virtual string PhoneNumber { get; init; }

        public virtual string Mail { get; init; }

        public virtual string HashPassword { get; init; }

        public virtual string Salt { get; init; }

        public virtual RoleOption Roles { get; init; }

        public virtual bool IsEmpty => Equals(new User());

        public static User Empty => new();

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id.Equals(other.Id) && Name == other.Name && Surname == other.Surname && PhoneNumber == other.PhoneNumber && Mail == other.Mail && HashPassword == other.HashPassword && Salt == other.Salt && Roles == other.Roles;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, string.Join('|', Name, Surname, PhoneNumber, Mail, HashPassword, Salt), (int)Roles);
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}
