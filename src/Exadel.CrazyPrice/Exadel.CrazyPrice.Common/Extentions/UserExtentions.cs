using Exadel.CrazyPrice.Common.Models;
using System;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Services.Common.Cryptography.Extentions;

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
            return user == null ? null : new Employee
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Mail = user.Mail
            };
        }

        /// <summary>
        /// Gets the UserInfo User entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserInfo ToUserInfo(this User user)
        {
            return user == null ? null : new UserInfo
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Mail = user.Mail,
                PhotoUrl = user.PhotoUrl,
                Language = user.Language
            };
        }

        /// <summary>
        /// Gets the Employee entity from User entity.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static User UpdateUser(this User user, UpdateUserRequest request)
        {
            
            var (hashPassword, salt) = request.Password.IsNullOrEmpty() ? (null, null) : request.Password.GetCryptPassword();
            return new User
            {
                Id = user.Id,
                HashPassword = hashPassword,
                Salt = salt,
                Language = request.Language,
                PhotoUrl = request.PhotoUrl,
                PhoneNumber = request.PhoneNumber
            };

        }

        /// <summary>
        /// Gets true when the Employee entity or id property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Employee employee)
        {
            return employee == null || employee.Id == Guid.Empty;
        }

        /// <summary>
        /// Gets true when the ExternalUser entity or mail property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="externalUser"></param>
        /// <returns></returns>
        public static bool IsEmpty(this AllowedExternalUser externalUser)
        {
            return externalUser == null || string.IsNullOrEmpty(externalUser.Mail);
        }

        /// <summary>
        /// Gets the User entity from User entity like Employee entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User ToUserLikeEmployee(this User user)
        {
            return user == null ? null : new User
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Mail = user.Mail,
                PhotoUrl = user.PhotoUrl,
                Language = user.Language
            };
        }
    }
}
