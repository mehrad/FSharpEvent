module NonNegativeDuration

type NonNegativeDuration = private NonNegativeDuration of int

let create (value: int) : Option<NonNegativeDuration> =
    if value >= 0 then Some(NonNegativeDuration value) else None

let value (NonNegativeDuration duration) = duration
