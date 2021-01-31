using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Data.Context;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbContext _context;

        public PersonRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetPersonByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }
    }
}
