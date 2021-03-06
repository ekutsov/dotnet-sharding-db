using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sharding.BusinessObjects.Settings;
using Sharding.Data.Context;
using Sharding.Data.Context.Factory;

namespace Sharding.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void EnsureShardsMigrationsRun(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                IShardDbContextFactory shardDbContextFactory = serviceScope.ServiceProvider.GetService<IShardDbContextFactory>();
                IOptions<List<ShardSettings>> shardSettings = serviceScope.ServiceProvider.GetService<IOptions<List<ShardSettings>>>();
                foreach (ShardSettings shard in shardSettings.Value)
                {
                    ShardDbContext context = shardDbContextFactory.CreateShardDbContext(shard);
                    context.Database.Migrate();
                }
            }
        }
    }
}