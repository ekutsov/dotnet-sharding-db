using Microsoft.EntityFrameworkCore;

using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context.Factory
{
    public class ShardDbContextFactory : IShardDbContextFactory
    {
        public ShardDbContext CreateShardDbContext(ShardSettings settings)
        {
            var options = new DbContextOptions<ShardDbContext>();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ShardDbContext>(options);
            return new ShardDbContext(dbContextOptionsBuilder.Options, settings);
        }
    }
}