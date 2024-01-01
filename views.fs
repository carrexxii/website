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
            a [ _class "btn"; _href "/post"     ] [ str "Post"    ]
        ]
    ]

let footer () =
    div [ _id "footer" ] [
        str "footer"
    ]

let addForm () =
    form [ _method "POST" ] [
        form [ _action "/post"; _method "POST" ] [
            input [ _id "postTitle"; _name "title"; _placeholder "Post Title" ]
            textarea [ _id "postContent"; _name "content"; _placeholder "Post content" ] []
            div [ _class "centre" ] [ input [ _type "submit"; _class "btn" ] ]
        ]
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
                ul [] (Seq.toList <| Seq.map
                    (fun t -> li [ _id "archive" ] [str t])
                    (Models.getPostList ())
                )
           ]
           <| footer ()

let post () =
    layout <| header ()
           <| div [ _id "body" ] [ addForm () ]
           <| footer ()
