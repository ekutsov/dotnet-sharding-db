using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context.Factory
{
    public class ShardDbContextFactory : IShardDbContextFactory
    {
        public ShardDbContext CreateDbContext(ShardSettings settings, IConfiguration configuration)
        {
            var options = new DbContextOptions<ShardDbContext>();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ShardDbContext>(options);
            return new ShardDbContext(dbContextOptionsBuilder.Options, configuration, settings);
        }
    }
}