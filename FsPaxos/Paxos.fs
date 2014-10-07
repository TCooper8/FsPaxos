namespace FsPaxos

open System
open System.Net
open Pario.Framework

module Paxos =
    type Data = byte array
    type Key = string

    type Username = string
    type UserId = int
    type Sender = UserId
    type WebAddress = string

    type ProposalN = int
    type ProposalId = int
    type Proposal = 
        | Proposal of ProposalId * ProposalN * Key * Data

    type InMsg =
        | Join of Username
        | VoteOn of Sender * Proposal

    type Reply =
        | VoteBallot of WebAddress * Sender * ProposalId

    type OutMsg =
        | ProposeVote of Proposal 

    type Replica(name:string) as this =
        inherit Actor<InMsg>()

        let logger =

        let id: Sender = name.GetHashCode()

        let mutable network = Map<UserId, WebAddress>([ ])

        let replyToRequest reply =
            match reply with
            | VoteBallot(target, sender, proposalId) ->
                let req = 
                    HttpWebRequest.CreateHttp(
                        sprintf "http://%s/voteBallot?sender=%i&proposalId=%i" target sender proposalId
                    )
                req.Method <- "POST"
                req

        let handleJoin username = ()

        let handleVoteOn proposal sender  =
            match proposal with
            | Proposal(id, n, key, data) ->
                let req = replyToRequest (VoteBallot(sender, this.Id, id))
                use resp = req.GetResponse()
                ()

        let sendReply senderId (reply: HttpWebRequest) =
            ()

        override this.Receive msg = 
            match msg with
            | Join(username) -> ()
            | VoteOn(sender, proposal) -> 
                match Map.tryFind sender network with
                | Some addr -> handleVoteOn proposal addr
                | None ->

        member this.Id = id

        member this.Start() = ()