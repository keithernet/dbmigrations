namespace MigrationRunner

module Finder =
    open Migrations
    open MigrationHelpers
    open System

    let private coerce value = (box >> unbox) value

    let private getMigrationAssemblyClasses (assemblies) = 
        assemblies 
            |> Array.find (fun (ass: Reflection.Assembly) -> ass.FullName.Contains("Migrations")) 
            |> (fun (a: Reflection.Assembly) -> a.GetTypes())

    let private getMigrationInstances (types) =
        types |> Array.filter (fun (t: Type) -> t.GetInterfaces() 
                                                    |> Array.exists (fun t -> t.ToString() = "MigrationHelpers.Migration"))
              |>  Array.map (fun a -> Activator.CreateInstance(a)
                                                 |> (fun i -> (coerce i: Migration)))

    let findMigrations (currentVersion: int) = 
        let t = Noop()
        let assemblies = AppDomain.CurrentDomain.GetAssemblies() 
        let types = getMigrationAssemblyClasses assemblies 

        getMigrationInstances types
            |> Array.filter (fun (m:Migration) -> m.Version > currentVersion)
            |> Array.sortBy (fun (m:Migration) -> m.Version)

    let getCurrentVersion = 
        Runner.runScalar(@"IF (EXISTS (SELECT * 
                                            FROM INFORMATION_SCHEMA.TABLES 
                                            WHERE TABLE_SCHEMA = 'dbo' 
                                            AND  TABLE_NAME = 'DbInfo'))
                                        BEGIN
                                            SELECT Version FROM dbo.DbInfo;
                                        END
                          ELSE 
                            SELECT -1")
