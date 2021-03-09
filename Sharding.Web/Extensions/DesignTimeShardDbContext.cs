using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sharding.BusinessObjects.Settings;
using Sharding.Data.Context;

namespace Sharding.Web.Extensions
{
    public class DesignTimeShardDbContext : IDesignTimeDbContextFactory<ShardDbContext>
    {
        public ShardDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.Development.json")
                                    .Build();
            var shardsSettings = configuration.GetSection("ShardsConnections").Get<List<ShardSettings>>();
            var optionsBuilder = new DbContextOptionsBuilder<ShardDbContext>();
            return new ShardDbContext(optionsBuilder.Options, shardsSettings[0].ConnectionString);
        }
    }
}