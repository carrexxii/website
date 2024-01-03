namespace Server

open Feliz

module Components =
    [<ReactComponent>]
    let button (text: string) =
        Html.form [
            Html.input [ prop.name "query" ]
            Html.button [ prop.type' "submit"; prop.text text ]
        ] |> ReactDOMServer.renderToString
