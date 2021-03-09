using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Extensions
{
    public static class MigrationSqlGeneratorExtensions
    {
        public static ShardSettings GetCurrentShard(this MigrationsSqlGeneratorDependencies dependencies)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.Development.json")
                                .Build();

            List<ShardSettings> shards = configuration.GetSection("ShardsConnections").Get<List<ShardSettings>>();
            string currentConnectionString = GetConnectionString(dependencies);
            return shards.Find(s => s.ConnectionString == currentConnectionString);
        }

        private static string GetConnectionString(MigrationsSqlGeneratorDependencies dependencies)
        {
            var context = dependencies.CurrentContext.Context;
            var connectionString =  (string)context.GetType().GetProperty("ConnectionString").GetValue(context);
            return connectionString;
        }
    }
}