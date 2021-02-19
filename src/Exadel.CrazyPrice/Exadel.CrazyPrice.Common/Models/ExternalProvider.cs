using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the ExternalUser.
    /// </summary>
    public class ExternalProvider
    {
        public ProviderOptions Provider { get; set; }
        public string UserId { get; set; }
    }
}
