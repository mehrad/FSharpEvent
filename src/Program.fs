open System
open NonNegativeDuration
open ScheduledEvent

let mutable events = System.Collections.Generic.Dictionary<Guid, ScheduledEvent>() 

let addEvent (event: ScheduledEvent) =
    events.Add(event.Id, event)

let viewEvents () =
    for event in events.Values do
        printfn "Event: %s" event.Title

let deleteEvent (eventId: Guid) =
    events.Remove(eventId)

let parseEvent (line: string) : Option<ScheduledEvent> =
    let parts = line.Split(',')
    if parts.Length <> 4 then None
    else
        let title = parts.[0]
        let description = parts.[1]
        let startDate = DateTime.Parse(parts.[2])
        let duration = NonNegativeDuration.create (int parts.[3]) |> Option.get
        Some(ScheduledEvent.create title description startDate duration)


let createEventsFromFile (filePath: string) : ScheduledEvent list =
    let lines = System.IO.File.ReadAllLines(filePath)
    lines |> Array.toList |> List.map parseEvent |> List.choose id



[<EntryPoint>]
let main argv =
    let duration = NonNegativeDuration.create 90 |> Option.get
    createEventsFromFile "events.txt" |> List.iter addEvent


    viewEvents()

    0 // Return an integer exit code
