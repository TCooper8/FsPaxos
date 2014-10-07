
[<EntryPoint>]
let main argv = 
    FsPaxos.Testing.parseUsernameTest 50 |> printfn "parseTest %A" |> ignore
    0 

