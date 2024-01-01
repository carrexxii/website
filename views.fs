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
        a [ _id "header"; _href "/" ] [ str "News From the High Seas" ]
        nav [ _id "navbar" ] [
            a [ _class "btn"; _href "/about"    ] [ str "About"   ]
            a [ _class "btn"; _href "/archive"  ] [ str "Archive" ]
            a [ _class "btn"; _href "/posts/-1" ] [ str "Random"  ]
            a [ _class "btn"; _href "/add"      ] [ str "Post"    ]
        ]
    ]

let footer () =
    div [ _id "footer" ] [
        str "footer"
    ]

let addForm () =
    form [ _method "POST" ] [
        input [ _id "postTitle"; _name "postTitle"; _placeholder "Post Title" ]
        textarea [ _id "postBody"; _name "postBody"; _placeholder "Post body" ] []
        div [ _class "centre" ] [ input [ _type "submit"; _class "btn" ] ]
    ]

let index post =
    layout <| header ()
           <| div [ _id "body" ] [ post ]
           <| footer ()

let about () =
    layout <| header ()
           <| div [ _id "body" ] [ str "about" ]
           <| footer ()

let archive () =
    layout <| header ()
           <| div [ _id "body" ] [
                h1 [] [ str "Archive of All Posts" ]
                ul [] [
                    li [ _id "archive" ]
                        (Seq.map (fun t -> str t)
                                 (Models.getPostList ())
                        |> Seq.toList)
                ]
           ]
           <| footer ()


let add () =
    layout <| header ()
           <| div [ _id "body" ] [ addForm () ]
           <| footer ()
