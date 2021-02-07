using Exadel.CrazyPrice.Common.Models;
using System;

namespace Exadel.CrazyPrice.Common.Extentions
{
    /// <summary>
    /// Represents extentions for User.
    /// </summary>
    public static class UserExtentions
    {

        /// <summary>
        /// Gets the Employee entity from User entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Employee ToEmployee(this User user)
        {
            return new()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Mail = user.Mail
            };
        }

        /// <summary>
        /// Gets true when the Employee entity or its property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Employee employee)
        {
            return employee == null || employee.Id == new Guid()
                && string.IsNullOrEmpty(employee.Name)
                && string.IsNullOrEmpty(employee.Surname)
                && string.IsNullOrEmpty(employee.Mail)
                && string.IsNullOrEmpty(employee.PhoneNumber);
        }
    }
}
