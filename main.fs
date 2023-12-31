open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection

open Giraffe
open Giraffe.ViewEngine

type Message = {
    Text: string
}

module Views =
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

let indexHandler id =
    let model = $"Requesting post #{id}"
    let view  = Views.index model
    htmlView view

let errorHandler (exn: Exception) (logger: ILogger) =
    logger.LogError (exn, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text exn.Message

let webApp =
    choose [
        GET >=>
            choose [
                route "/" >=> indexHandler -1
                routef "/post/%i" indexHandler
            ]
        setStatusCode 404 >=> text "Not Found"
    ]

let configureCors (builder: CorsPolicyBuilder) =
    builder
        .WithOrigins(
            "http://localhost:5000",
            "https://localhost:5001")
       .AllowAnyMethod()
       .AllowAnyHeader()
       |> ignore

let configureApp (app: IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IWebHostEnvironment> ()
    (match env.IsDevelopment () with
    | true  -> app.UseDeveloperExceptionPage ()
    | false ->
        app.UseGiraffeErrorHandler(errorHandler)
           .UseHttpsRedirection ())
        .UseCors(configureCors)
        .UseStaticFiles()
        .UseGiraffe webApp

let configureServices (services: IServiceCollection) =
    services.AddCors ()    |> ignore
    services.AddGiraffe () |> ignore

let configureLogging (builder: ILoggingBuilder) =
    builder.AddConsole()
           .AddDebug () |> ignore

[<EntryPoint>]
let main args =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(fun webHostBuilder ->
            webHostBuilder
                .UseContentRoot(contentRoot)
                .UseWebRoot(webRoot)
                .Configure(Action<IApplicationBuilder> configureApp)
                .ConfigureServices(configureServices)
                .ConfigureLogging(configureLogging)
                |> ignore)
        .Build()
        .Run ()
    0
