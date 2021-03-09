using Microsoft.EntityFrameworkCore.Migrations;

namespace Sharding.Data.Extensions
{
    public static class ConstraintExtensions
    {
        public static MigrationCommandListBuilder AddConstraint(this MigrationCommandListBuilder builder,
                                                                string tableName,
                                                                string constraintName,
                                                                string constraint)
        {
            return builder.Append($"alter table {tableName} add constraint {constraintName} check ({constraint})");
        }
    }
}