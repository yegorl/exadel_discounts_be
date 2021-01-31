using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbContext _context;

        public CompanyRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetCompanyNamesAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
