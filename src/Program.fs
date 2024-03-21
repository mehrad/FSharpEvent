open System
open NonNegativeDuration
open ScheduledEvent
open System.IO

let initialEvents = Seq.empty<ScheduledEvent>

let addEvent event eventsSeq =
    seq {
        yield! eventsSeq  // Yield all events currently in the sequence
        yield event       // Add the new event to the sequence
    }

let viewEvents eventsSeq =
    eventsSeq |> Seq.iter (fun e -> 
        printfn "Id: %A, Title: %s, Start Date: %O, Duration: %d minutes" e.Id e.Title e.StartDate (NonNegativeDuration.value e.Duration))

let deleteEvent eventId eventsSeq =
    eventsSeq |> Seq.filter (fun e -> e.Id <> eventId)

let parseEvent (line: string) : Option<ScheduledEvent> =
    let parts = line.Split(',')
    if parts.Length = 4 then
        let title = parts.[0]
        let description = parts.[1]
        let startDate = DateTime.Parse(parts.[2])
        let duration = NonNegativeDuration.create (int parts.[3])
        match duration with
        | Some d -> Some (ScheduledEvent.create title description startDate d)
        | None -> None
    else
        None


let createEventsFromFile (filePath: string) : seq<ScheduledEvent> =
    File.ReadLines(filePath)
    |> Seq.map parseEvent
    |> Seq.choose id

[<EntryPoint>]
let main argv =
    createEventsFromFile "events.txt"
    |> viewEvents

    0 // return an integer exit code