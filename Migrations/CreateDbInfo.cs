using System;
using MigrationHelpers;

namespace Migrations
{
    class CreateDbInfo : Migration
    {
        public string Command => @"CREATE TABLE dbo.DbInfo (Version int); 
                                   INSERT INTO dbo.DbInfo Values (0);";

        public int Version => 0;
    }
}
