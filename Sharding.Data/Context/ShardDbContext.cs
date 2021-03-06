using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Sharding.BusinessObjects.Models;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context
{
    public class ShardDbContext : DbContext
    {
        private readonly ShardSettings _shard;
        public ShardDbContext(DbContextOptions<ShardDbContext> options, ShardSettings shard) : base(options)
        {
            _shard = shard;
        }

        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_shard.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}