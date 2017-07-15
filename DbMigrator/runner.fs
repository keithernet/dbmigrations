namespace MigrationRunner

module public Runner =
    open System.Data.SqlClient
    open System.Configuration
    open System.Transactions
    open MigrationHelpers


    let connString = (System.Configuration.ConfigurationManager.ConnectionStrings.Item("target")).ConnectionString

    let runMigration (migration : Migration)=
        use transaction = new TransactionScope()
        use conn = new SqlConnection(connString)
        conn.Open()
        let cmd = new SqlCommand(migration.Command, conn)
        cmd.ExecuteNonQuery() |> ignore
        let versionUpdate = new SqlCommand("UPDATE dbo.DbInfo SET Version = @version",conn)
        versionUpdate.Parameters.AddWithValue("@version",migration.Version) |> ignore
        versionUpdate.ExecuteNonQuery() |> ignore
        transaction.Complete()
        migration.Version

    let runScalar (query : string) =
        use conn = new SqlConnection(connString)
        conn.Open()
        let cmd = new SqlCommand(query, conn)
        cmd.ExecuteScalar() :?> int
        

        
