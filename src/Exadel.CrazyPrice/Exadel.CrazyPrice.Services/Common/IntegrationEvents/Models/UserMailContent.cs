using Exadel.CrazyPrice.Common.Models;
using System;

namespace Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models
{
    public record UserMailContent(Employee Employee, Guid DiscountId, string DiscountName, string PromocodeValue);
}
