using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sharding.BusinessObjects.Settings;
using Sharding.Data.Context;

namespace Sharding.Core.Configuration
{
    public static class Dependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSettings(configuration);
            services.RegisterDbContext(configuration);
        }

        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<ShardSettings>>(configuration.GetSection("ShardsConnections"));
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RootDbContext>
                (optionBuilder => optionBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}