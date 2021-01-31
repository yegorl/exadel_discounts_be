using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDbContext _context;

        public DiscountRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<DiscountResponse>> GetDiscountsAsync(SearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public async Task<DiscountResponse> GetDiscountByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task<UpsertDiscountRequest> UpsertDiscountAsync(UpsertDiscountRequest item)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDiscountByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDiscountAsync(List<Guid> uids)
        {
            throw new NotImplementedException();
        }
    }
}
