using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;
using Sharding.BusinessObjects.Settings;
using Sharding.Data.Configuration;
using Sharding.Data.Context;

namespace Sharding.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void EnsureShardsMigrationsRun(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            var shardSettings = services.GetService<IOptions<List<ShardSettings>>>();
            foreach (var shard in shardSettings.Value)
            {
                var builder = new DbContextOptionsBuilder<ShardDbContext>();
                var context = new ShardDbContext(builder.Options, shard.ConnectionString);
                context.Database.Migrate();
            }
        }
    }
}