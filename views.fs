module Views

open Giraffe.ViewEngine

let layout content =
    html [] [
        head [] [
            title []  [ encodedText "website" ]
            link [ _rel  "stylesheet"
                   _type "text/css"
                   _href "/styles.css" ]
        ]
        body [] content
    ]

let partial () =
    h1 [] [ encodedText "website" ]

let index model =
    [
        partial()
        p [] [ encodedText model ]
    ] |> layout
