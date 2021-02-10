namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the address.
    /// </summary>
    public class Address
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public Location Location { get; set; }
    }
}