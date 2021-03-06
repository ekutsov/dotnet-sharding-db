using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context.Factory
{
    public interface IShardDbContextFactory
    {
        ShardDbContext CreateShardDbContext(ShardSettings settings);
    }
}