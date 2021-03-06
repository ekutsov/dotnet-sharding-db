using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sharding.BusinessObjects.Settings;
using Sharding.Data.Context;
using Sharding.Data.Context.Factory;

namespace Sharding.Core.Configuration
{
    public static class Dependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSettings(configuration);
            services.RegisterRootDbContext(configuration);
            services.RegisterFactories();
        }

        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<List<ShardSettings>>(configuration.GetSection(ShardSettings.Name));
        }

        public static void RegisterRootDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RootDbContext>(optionBuilder =>
                optionBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void RegisterFactories(this IServiceCollection services)
        {
            services.AddTransient<IShardDbContextFactory, ShardDbContextFactory>();
        }
    }
}