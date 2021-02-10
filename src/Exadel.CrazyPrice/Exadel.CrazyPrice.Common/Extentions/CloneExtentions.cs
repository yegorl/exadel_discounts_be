using Exadel.CrazyPrice.Common.Models;

namespace Exadel.CrazyPrice.Common.Extentions
{
    /// <summary>
    /// Represents clone methods for entities.
    /// </summary>
    public static class CloneExtentions
    {
        /// <summary>
        /// Clones the Address entity.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static Address Clone(this Address address, Discount discount)
        {
            return address == null ? null : new Address
            {
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                Location = discount.Address.Location.Clone()
            };
        }

        /// <summary>
        /// Clones the Location entity.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Location Clone(this Location location)
        {
            return location == null ? null : new Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        /// <summary>
        /// Clones the Company entity.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static Company Clone(this Company company, Discount discount)
        {
            return company == null ? null : new Company
            {
                Name = company.Name,
                Description = company.Description,
                PhoneNumber = discount.Company.PhoneNumber,
                Mail = discount.Company.Mail
            };
        }
    }
}
