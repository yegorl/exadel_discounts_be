using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Option;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbExternalUser
    {
        public ProviderOptions ProviderName { get; set; }
        public string UserId { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
