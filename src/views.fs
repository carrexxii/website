namespace Server

open Feliz
open Feliz.ViewEngine
open Zanaptak.TypedCssClasses

module Views =
    let [<Literal>] frameworkCSS = "static/bootstrap.min.css"
    let [<Literal>] frameworkJS  = "bootstrap.bundle.min.js"
    type CSS       = CssClasses<"client/static/bootstrap.min.css", Naming.Underscores>
    type CustomCSS = CssClasses<"client/static/styles.css", Naming.Underscores>

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    let a (text: string) href classes =
        Html.a [
            prop.classes classes
            prop.href href
            prop.text text
        ]

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    let ofPost (post: Models.Post) =
        Html.div [
            prop.children [
            Html.h1 [ prop.id "title"; prop.text post.title ]
            ]
            prop.text post.content
        ]

    let header () =
        let navBtn (text: string) link =
            Html.li [
                prop.classes [ CSS.nav_item ]
                prop.children [ a text link [ CSS.nav_link ] ]
            ]

        Html.div [
            prop.classes [ CSS.p_5; CSS.bg_primary; CSS.text_center ]
            prop.children [
            Html.h1 [
                prop.children [ a "News From the High Seas" "/" [ CustomCSS.pirata_font; CSS.text_light ] ]
            ]
            Html.nav [
                prop.classes [ CSS.navbar; CSS.navbar_expand_lg; CSS.justify_content_center ]
                prop.children [
                Html.ul [
                    prop.classes [ CSS.nav ]
                    prop.children [
                    navBtn "Home"    "/"
                    navBtn "Random"  "/posts/-1"
                    navBtn "Archive" "/archive"
                    navBtn "Post"    "/post"
                    ]
                ]
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
        let loadScript name =
            Html.script [ prop.type' "module"; prop.src $"js/{name}" ]

        Html.html [
            prop.lang "en"
            prop.children [
            Html.head [
                Html.meta [ prop.charset "utf-8" ]
                Html.meta [ prop.name "viewport"; prop.content "width=device-width, initial-scale=1" ]

                loadScript "react.js"
                loadScript "react-dom.js"
                loadScript "components.js"
                loadScript frameworkJS

                Html.link [
                    prop.rel   "stylesheet"
                    prop.type' "text/css"
                    prop.href  frameworkCSS
                ]
                Html.link [
                    prop.rel   "stylesheet"
                    prop.type' "text/css"
                    prop.href  "static/styles.css"
                ]

                Html.title "AIBeard's News"
            ]
            Html.body [
                prop.classes [ CSS.row; CSS.justify_content_center ]
                prop.children [
                header ()

                Html.div [ prop.classes [ CSS.col; CSS.col_sm_2 ] ]
                Html.div [
                    prop.classes [ CSS.col; CSS.col_sm_8 ]
                    prop.children [

                    content
                    match script with
                    | Some s -> loadScript s
                    | None   -> ()

                    footer ()
                    ]
                ]
                Html.div [ prop.classes [ CSS.col; CSS.col_sm_2 ] ]
                ]
            ]
            ]
        ]

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        <| Html.div [ prop.id "root" ]
        <| Some "post.js"
