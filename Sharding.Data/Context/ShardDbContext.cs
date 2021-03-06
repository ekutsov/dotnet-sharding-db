using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sharding.BusinessObjects.Models;
using Sharding.BusinessObjects.Settings;
using Sharding.Data.Context.Provider;

namespace Sharding.Data.Context
{
    public class ShardDbContext : DbContext
    {
        private readonly ShardSettings _shard;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        public ShardDbContext(DbContextOptions<ShardDbContext> options,
                              IConfiguration configuration,
                              ShardSettings shard) : base(options)
        {
            _shard = shard;
            _configuration = configuration;
        }

        public ShardDbContext(DbContextOptions<ShardDbContext> options,
                              IConfiguration configuration,
                              IShardProvider shardProvider,
                              ILoggerFactory loggerFactory)
                              : base(options)
        {
            _configuration = configuration;
            _shard = shardProvider.GetShard();
            _loggerFactory = loggerFactory;
        }

        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .UseNpgsql(_shard.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}