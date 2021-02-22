using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the ExternalUser.
    /// </summary>
    public class ExternalUser
    {
        public ProviderOptions ProviderName { get; set; }
        public string UserId { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
