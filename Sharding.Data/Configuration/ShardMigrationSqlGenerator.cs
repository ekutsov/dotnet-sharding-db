using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

using Sharding.BusinessObjects.Constants;
using Sharding.BusinessObjects.Settings;
using Sharding.BusinessObjects.Static;
using Sharding.Data.Extensions;

namespace Sharding.Data.Configuration
{
    public class ShardMigrationSqlGenerator : NpgsqlMigrationsSqlGenerator
    {
        private readonly ShardSettings _currentShard;
        public ShardMigrationSqlGenerator(MigrationsSqlGeneratorDependencies dependencies,
                                          INpgsqlOptions npgsqlOptions) : base(dependencies, npgsqlOptions)
        {
            _currentShard = dependencies.GetCurrentShard();
        }

        protected override void Generate(CreateTableOperation operation, IModel model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            base.Generate(operation, model, builder);
            switch (operation.Name)
            {
                case ShardTable.News:
                    AddNewsConstraints(operation, builder);
                    break;
                default:
                    break;
            }
        }

        private void AddNewsConstraints(CreateTableOperation createTableOperation, MigrationCommandListBuilder builder)
        {
            switch (_currentShard.Name)
            {
                case Shards.FirstShard:
                    builder.AddConstraint(createTableOperation.Name, "ck_year", "year = 2020").EndCommand();
                    break;
                case Shards.SecondShard:
                    builder.AddConstraint(createTableOperation.Name, "ck_year", "year = 2021").EndCommand();
                    break;
                default:
                    break;
            }
        }
        // private void Generate(CreateTableOperation operation, MigrationCommandListBuilder builder)
        // {
        //     builder
        //         .AppendLine("CREATE EXTENSION IF NOT EXISTS postgres_fdw;")
        //         .AppendLine("CREATE SERVER IF NOT EXISTS first_api_shard_one_db")
        //         .AppendLine("FOREIGN DATA WRAPPER postgres_fdw")
        //         .AppendLine("OPTIONS (dbname 'first_api_shard_one_db', port '6432', host '172.23.0.1');")
        //         // .AppendLine("CREATE USER MAPPING IF NOT EXISTS FOR first_api_root_user")
        //         // .AppendLine("SERVER first_api_shard_one_db")
        //         // .AppendLine("OPTIONS (user 'first_api_shard_one_user', password 'first_api_shard_one_password');")
        //         // .AppendLine("CREATE FOREIGN TABLE IF NOT EXISTS 'News1' (")
        //         // .AppendLine("id bigint not null,")
        //         // .AppendLine("year int not null,")
        //         // .AppendLine("title character varying")
        //         // .AppendLine(") server first_api_shard_one_db")
        //         // .AppendLine("options (schema_name 'public', table_name 'News');")
        //         .EndCommand();
        // }
    }
}
