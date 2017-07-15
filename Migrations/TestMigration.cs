using MigrationHelpers;

namespace Migrations
{
    class TestMigration : Migration
    {
        public string Command => "ALTER TABLE dbo.DbInfo ADD Updated int";

        public int Version => 1;
    }
}
