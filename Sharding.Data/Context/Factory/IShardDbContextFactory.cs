using Microsoft.Extensions.Configuration;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context.Factory
{
    public interface IShardDbContextFactory
    {
        ShardDbContext CreateDbContext(ShardSettings settings, IConfiguration confifuration);
    }
}