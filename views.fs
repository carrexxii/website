module Views

open Giraffe.ViewEngine

let layout header content footer =
    html [] [
        head [] [
            title [] [ str "AIBeard's News" ]
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

let header () =
    div [ _id "header" ] [
        h1 [ _id "header" ] [ str "News From the High Seas" ]
        nav [ _id "navbar" ] [
            a [ _class "navbtn"; _href "/about"    ] [ str "About"   ]
            a [ _class "navbtn"; _href "/archive"  ] [ str "Archive" ]
            a [ _class "navbtn"; _href "/posts/-1" ] [ str "Random"  ]
        ]
    ]

let footer () =
    div [ _id "footer" ] [
        str "footer"
    ]

let index post =
    layout <| header ()
           <| div [ _id "body" ] [ post ]
           <| footer ()
