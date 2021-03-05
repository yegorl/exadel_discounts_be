using Exadel.CrazyPrice.Common.Models;
using System;

namespace Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models
{
    public record CompanyMailContent(Company Company, Guid DiscountId, string DiscountName, string PromocodeValue);
}