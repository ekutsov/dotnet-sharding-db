using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sharding.BusinessObjects.Settings;
using Sharding.Data.Configuration;
using Sharding.Data.Context;

namespace Sharding.Core.Configuration
{
    public static class Dependecies
    {
        public static void RegisterDependecies(this IServiceCollection services)
        {
            // services.RegisterSettings(configuration);
        }

        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            
        }
    }
}