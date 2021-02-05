using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    public interface ISeed
    {
        string CollectionName { get; }

        Task<string> StatusTotalAsync();

        Task CreateIndexesAsync();

        Task DeleteIfSetAsync();

        Task SeedAsync();
    }
}