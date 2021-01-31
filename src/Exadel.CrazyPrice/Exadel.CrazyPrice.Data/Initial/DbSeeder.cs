using Exadel.CrazyPrice.Data.Context;
using System;

namespace Exadel.CrazyPrice.Data.Initial
{
    public class DbSeeder
    {
        private readonly IDbContext _dbContext;

        public DbSeeder(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            throw new NotImplementedException();
        }
    }
}
