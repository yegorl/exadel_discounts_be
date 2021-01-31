using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDbContext _context;

        public AddressRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetCountriesAsync(string searchCountry)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetCitiesAsync(string searchCountry, string searchCity)
        {
            throw new NotImplementedException();
        }
    }
}
