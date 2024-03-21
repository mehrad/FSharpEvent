module ScheduledEvent

open System

type ScheduledEvent = {
    Id: Guid
    Title: string
    Description: string
    StartDate: DateTime
    Duration: NonNegativeDuration.NonNegativeDuration
}

let create (title: string) (description: string) (startDate: DateTime) (duration: NonNegativeDuration.NonNegativeDuration) =
    { Id = Guid.NewGuid(); Title = title; Description = description; StartDate = startDate; Duration = duration }
