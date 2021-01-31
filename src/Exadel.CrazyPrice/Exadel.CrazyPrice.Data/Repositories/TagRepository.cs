using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IDbContext _context;

        public TagRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetTagAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
