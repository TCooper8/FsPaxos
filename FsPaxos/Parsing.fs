namespace FsPaxos

open System

module Parsing = 
    let (|Int|_|) input =
        let mutable x = 0
        match Int32.TryParse(input, &x) with
        | true -> Some x
        | false -> None

    let (|Float|_|) input =
        let mutable x = 0.0
        match Double.TryParse(input, &x) with
        | true -> Some x
        | false -> None

    let (|KeyValue|_|) startsWith (input:string) =
        if input.StartsWith(startsWith)
        then 
            let i, j = input.IndexOf("("), input.LastIndexOf(")")
            if i <> -1 && j <> -1
            then input.Substring(i+1, j-i-1) |> Some
            else None
        else None

    let (|Username|_|) (input:string): Paxos.Username option =
        match input with
        | KeyValue "Username" v -> Some v
        | _ -> None
        (*if input.StartsWith("Username(")
        then
            let i, j = input.IndexOf("("), input.LastIndexOf(")")
            if i <> -1 && j <> -1
            then input.Substring(i+1, j-i-1) |> Some
            else None
        else None*)

    let (|UserId|_|) (input:string): Paxos.UserId option =
        match input with
        | KeyValue "UserId" (Int v) -> Some v
        | _ -> None
