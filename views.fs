module Views

open Giraffe.ViewEngine

let layout header content footer =
    html [] [
        head [] [
            title [] [ encodedText "website" ]
            link [
                _rel  "stylesheet"
                _type "text/css"
                _href "/styles.css"
            ]
        ]
        body [] [
            header
            content
            footer
        ]
    ]

let partial () =
    h1 [] [ encodedText "website" ]

let header () =
    div [ _id "header" ] [
        h1 [] [ encodedText "Website Header" ]
        nav [ _id "navbar" ] [
            a [ _class "navbtn"; _href "/about" ] [ encodedText "About" ]
            a [ _class "navbtn"; _href "/archive" ] [ encodedText "Archive" ]
            a [ _class "navbtn"; _href "/posts/-1" ] [ encodedText "Random" ]
        ]
    ]

let footer () =
    div [ _id "footer" ] [
        encodedText "footer"
    ]

let index (post: Models.Post) =
    layout <| header ()
           <| div [ _id "body" ] [ encodedText post.content ]
           <| footer ()
