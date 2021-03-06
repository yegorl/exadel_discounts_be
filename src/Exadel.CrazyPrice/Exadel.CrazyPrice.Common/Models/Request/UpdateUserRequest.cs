using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models.Request
{
    public class UpdateUserRequest
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }
        public LanguageOption Language { get; set; }
    }
}
