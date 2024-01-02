namespace Server

open Feliz
open Feliz.ViewEngine

module Views =
    let ofPost (post: Models.Post) =
        Html.div [
            // prop.children [
            // Html.h1 [ prop.id "title"; prop.text post.title ]
            // ]
            // prop.text post.content
        ]

    let header () =
        Html.div [
            prop.id "header"
            prop.children [
            Html.a [
                prop.id "header"; prop.href "/"
                prop.text "News From the High Seas"
            ]
            Html.nav [
                prop.id "navbar"
                prop.children [
                Html.a [ prop.className "btn"; prop.href "/about"   ; prop.text "About"   ]
                Html.a [ prop.className "btn"; prop.href "/archive" ; prop.text "Archive" ]
                Html.a [ prop.className "btn"; prop.href "/posts/-1"; prop.text "Random"  ]
                Html.a [ prop.className "btn"; prop.href "/post"    ; prop.text "Post"    ]
                ]
            ]
            ]
        ]

    let footer () =
        Html.div [
            prop.id "footer"
            prop.text "footer"
        ]

    let layout (content: ReactElement) script =
        Html.html [
            Html.head [
                Html.title "AIBeard's News"
                Html.link [
                    prop.rel   "stylesheet"
                    prop.type' "text/css"
                    prop.href  "/styles.css"
                ]
            ]
            Html.body [
                header ()
                content
                Html.script [ prop.type' "module"; prop.src "react.js" ]
                Html.script [ prop.type' "module"; prop.src "react-dom.js" ]
                Html.script [ prop.type' "module"; prop.src "components.js" ]
                match script with
                | Some script -> Html.script [ prop.type' "module"; prop.src $"{script}.js" ]
                | None -> ()
                footer ()
            ]
        ]

    let postForm () =
        Html.form [
            prop.method "POST"
            prop.children [
            Html.form [
                prop.action "/post"
                prop.method "POST"
                prop.children [
                Html.input [
                    prop.id "postTitle"
                    prop.name "title"
                    prop.placeholder "Post Title"
                ]
                Html.textarea [
                    prop.id "postContent"
                    prop.name "content"
                    prop.placeholder "Post content"
                ]
                Html.div [
                    prop.className "centre"
                    prop.children [
                        Html.input [ prop.type' "submit"; prop.className "btn" ] ]
                ]
                ]
            ]
            ]
        ]

    let index post =
        layout
        <| Html.div [ prop.id "root"; prop.children [ post ] ]
        <| None

    let about () =
        layout
        <| Html.div [ prop.id "root"; prop.text "about" ]
        <| None

    let archive () =
        layout
        <| Html.div [
            prop.id "root"
            prop.children [
            Html.h1 "Archive of All Posts"
            Html.ul (Seq.toList <| Seq.map
                (fun (t: string) ->
                    Html.li [ prop.id "archive"; prop.text t ])
                (Models.getPostList ()))
            ]
        ]
        <| None

    let post () =
        layout
        <| Html.div [ prop.id "root"; prop.children [postForm ()] ]
        <| Some "post"