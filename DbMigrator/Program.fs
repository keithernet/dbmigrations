open System
open Migrations
open MigrationHelpers
open MigrationRunner

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.


[<EntryPoint>]
let main argv = 
    try
        let version = Finder.getCurrentVersion
        let migrations = Finder.findMigrations version
        let toRun = match migrations.Length with
            | 0 -> printfn "Up to date"; [||];
            | c when c > 0  -> migrations |> Array.filter (fun i -> i.Version > version) 
        
        toRun |> Array.iter (fun i -> 
                                printfn "Attempting to run %A" (i.GetType().Name)
                                printfn "Updated to version: %A\n" (Runner.runMigration(i)))
    with e -> printfn "Failed with exception: %A" e 
    Console.Read()