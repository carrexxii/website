namespace Server

open Feliz
open Feliz.ViewEngine
open Zanaptak.TypedCssClasses

module Views =
    let [<Literal>] frameworkCSS = "lib/bootstrap.min.css"
    let [<Literal>] frameworkJS  = "lib/bootstrap.bundle.min.js"
    type CSS = CssClasses<"client/lib/bootstrap.min.css", Naming.Underscores>
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
        Html.html [
            prop.lang "en"
            prop.children [
            Html.head [
                Html.meta [ prop.charset "utf-8" ]
                Html.meta [ prop.name "viewport"; prop.content "width=device-width, initial-scale=1" ]

                let script name = Html.script [ prop.type' "module"; prop.src name ]
                script "lib/react.js"
                script "lib/react-dom.js"
                script "components.js"
                script frameworkJS

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
                    | Some script -> Html.script [ prop.type' "module"; prop.src $"{script}.js" ]
                    | None -> ()

                    footer ()
                    ]
                ]
                Html.div [ prop.classes [ CSS.col; CSS.col_sm_2 ] ]
                ]
            ]
            ]
        ]

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    let postForm () =
        Html.div [
            prop.children [
            Html.form [
                prop.classes [ CSS.form_floating; CSS.m_5 ]
                prop.children [
                Html.input [
                    prop.classes     [ CSS.form_control; CSS.p_3; CSS.pt_5; CSS.w_75; CSS.fs_3 ]
                    prop.id          "titleForm"
                    prop.placeholder "Post Title"
                ]
                Html.label [
                    prop.for' "titleForm"
                    prop.text "Post Title"
                ]
                ]
            ]
            Html.form [
                prop.classes [ CSS.form_floating; CSS.m_5 ]
                prop.children [
                Html.textarea [
                    prop.classes     [ CSS.form_control; CSS.px_3 ]
                    prop.id          "titleContent"
                    prop.placeholder "Post Content"
                    prop.style [ style.height 300 ]
                ]
                Html.label [
                    prop.for' "titleContent"
                    prop.text "Post Content"
                ]
                ]
            ]
            Html.button [
                prop.type'   "submit"
                prop.classes [ CSS.btn; CSS.btn_primary; CSS.float_end; CSS.py_2; CSS.px_4; CSS.m_5 ]
                prop.text    "Submit"
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
        <| Html.div [ prop.id "root"; prop.children [postForm ()] ]
        <| None
