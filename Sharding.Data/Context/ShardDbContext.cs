using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

using Sharding.BusinessObjects.Models;
using Sharding.Data.Configuration;
using Sharding.Data.Extensions;

namespace Sharding.Data.Context
{
    public class ShardDbContext : DbContext
    {
        public string ConnectionString { get; }
        public ShardDbContext(DbContextOptions<ShardDbContext> options, string connectionString) : base(options)
        {
            ConnectionString = connectionString;
        }

        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseNpgsql(ConnectionString)
                .ReplaceService<IMigrationsSqlGenerator, ShardMigrationSqlGenerator>();
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSnakeCaseNames();
            base.OnModelCreating(modelBuilder);
        }
    }
}