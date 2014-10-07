namespace FsPaxos

open System
open System.IO
open System.Text
open Parsing
open Pario.Framework

module Testing =
    let rand = new Random()
    let encoding = new UTF8Encoding()

    let test tests mapping =
        Seq.fold (fun acc test -> 
            match mapping test with
            | Success v -> Success()
            | Failure e -> Failure(e)
        ) (Success()) tests

    let parseUsernameTest testN =
        let iter i = 
            let s = sprintf "%i" i
            s, sprintf "Username(%s)" s
        let tests = seq { for i in 1..testN do yield iter i }

        let mapping (expect:string, input:string) =
            match input with
            | Parsing.Username(name) -> 
                if expect = name
                then Success()
                else new Exception(sprintf "Expected: %s Got %s" expect name) |> Failure
            | _ -> Failure (new Exception(sprintf "Input did not match %s to %s" input expect))

        test tests mapping 

