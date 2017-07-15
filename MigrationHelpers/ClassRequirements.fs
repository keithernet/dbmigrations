namespace MigrationHelpers

open System

type public Migration =
    abstract Command : string with get
    abstract Version: int with get