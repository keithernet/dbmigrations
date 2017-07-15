using System;
using MigrationHelpers;

namespace Migrations
{
    class Version3 : Migration
    {
        public string Command => "UPDATE dbo.DbInfo SET Updated = 1";

        public int Version => 2;
    }
}
