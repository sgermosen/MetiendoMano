// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace FabulousTravel

open System.Diagnostics
open System
open Fabulous
open Fabulous.XamarinForms.LiveUpdate
open Fabulous.XamarinForms
open Xamarin.Forms

module App = 
    type City =
        { Name : string
          Country : string
          Image : string
          Rating : decimal
          IsFavorite : bool }

    type Model = 
      { CurrentCity : City
        Cities : City list }

    type Msg = 
        | City of City 
        | ToggleFavorite of City 

    let zurich = { Name = "Zurich"; Image = "Zurich"; Country = "Switzerland"; Rating = 4.5m; IsFavorite = true}
    let london = { Name = "London"; Image = "London"; Country = "UK"; Rating = 4.8m; IsFavorite = false}
    let seattle = { Name = "Seattle"; Image = "Seattle"; Country = "USA"; Rating = 5m; IsFavorite = false}

    let initModel = { CurrentCity = zurich; Cities = [zurich; london; seattle] }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | City selectedCity -> { model with CurrentCity = selectedCity }, Cmd.none
        | ToggleFavorite city -> { model with CurrentCity = {city with IsFavorite = not city.IsFavorite } }, Cmd.none

    let magnify = "\uf349"
    let star = "\uf4ce"
    let heartFilled = "\uf2d1"
    let heartOutline = "\uf2d5"

    let cornerRadius = 22.

    let titleFontSize = FontSize 20.
    let cardTitleFontSize = FontSize 16.
    let descriptionFontSize = FontSize 14.

    let textColor = Color.Black
    let secondaryTextColor = Color.FromHex "FFB5B5B5"
    let backgroundColor = Color.White
    let starColor = Color.FromHex "FFFFBF00"
    let favoriteColor = Color.FromHex "FFFAACC1"

    let materialFont =
        (match Device.RuntimePlatform with
                                 | Device.iOS -> "Material Design Icons"
                                 | Device.Android -> "materialdesignicons-webfont.ttf#Material Design Icons"
                                 | _ -> null)

    let titleLabel text fontSize =
        View.Label(text = text,
            fontSize = fontSize,
            textColor = textColor,
            verticalOptions = LayoutOptions.Center,
            fontAttributes = FontAttributes.Bold)

    let descriptionLabel text =
        View.Label(text = text,
            textColor = secondaryTextColor,
            fontSize = descriptionFontSize
            )

    let titleAndDescription title titleFontSize description =
        View.StackLayout(margin = Thickness 0.,
            children=[
                titleLabel title titleFontSize
                descriptionLabel description |> fun(label) -> label.Margin (Thickness(0.,-8.,0.,0.))]
                )

    let materialButton materialIcon backgroundColor textColor command =
        View.Button(text = materialIcon,
            command = command,
            fontFamily = materialFont,
            fontSize = titleFontSize,
            backgroundColor = backgroundColor,
            //widthRequest = 42.,
            textColor = textColor)

    let materialIcon materialIcon color =
        View.Label(text = materialIcon,
            textColor = color,
            fontFamily = materialFont,
            fontSize = FontSize 18.,
            verticalOptions = LayoutOptions.Center,
            fontAttributes = FontAttributes.Bold)

    let ratingStar percentage =
        let star = materialIcon star starColor
        let boxViewWidth = (16. - (16. * percentage))
        View.Grid(
            padding = Thickness 0.,
            margin = Thickness(0.,-4.,0.,0.),
            children = [
                star
                View.BoxView(color = backgroundColor, 
                    width = boxViewWidth,
                    isVisible = (if percentage > 0. then true else false),
                    horizontalOptions = LayoutOptions.End)
                ])

    let ratingControl (rating:decimal) =
        let fullNumber = Math.Ceiling(rating)
        let fraction = (rating - Math.Truncate(rating))
        View.StackLayout(orientation = StackOrientation.Horizontal,
            children = [
                for i in 1m .. fullNumber -> if i = fullNumber then ratingStar (float fraction) else ratingStar 1.
            ])

    let favoriteIcon city dispatch =
        let icon = if city.IsFavorite then heartFilled else heartOutline
        (materialButton icon Color.Transparent favoriteColor (fun () -> dispatch (ToggleFavorite city)))
        |> fun(button) -> button.HorizontalOptions LayoutOptions.End
        |> fun(button) -> button.Margin (Thickness(0.,-8.,-28.,0.))
        |> fun(button) -> button.HeightRequest 8.
        |> fun(button) -> button.FontSize (FontSize 32.)

    let roundedCornerImage imagePath =
        View.Frame(cornerRadius = cornerRadius,
            padding = Thickness 0.,
            isClippedToBounds = true,
            hasShadow = true,
            content = View.Image(
                source = imagePath,
                aspect = Aspect.AspectFill)
        )
        
    let cityDescriptionFrame city dispatch =
        View.StackLayout(
            margin = Thickness(16.,0.,16.,0.),
            children = [
                (roundedCornerImage (Path city.Image) |> fun(img) -> img.HeightRequest 320.)
                View.Frame(
                    height = 70.,
                    margin = Thickness(24.,-64.,24.,0.),
                    padding = Thickness(20.,12.,16.,12.),
                    backgroundColor = Color.White,
                    cornerRadius = cornerRadius,
                    content = View.Grid(
                        rowdefs=[Auto; Auto ],
                        coldefs=[Star;Auto],
                        children=[
                            (titleAndDescription city.Name titleFontSize city.Country)
                            (favoriteIcon city dispatch).Column(2)
                            (ratingControl city.Rating).Row(1).ColumnSpan(2)
                            ]
                    ),
                    hasShadow = true)
            ])

    let activityFrame title description image =
        View.Frame(
            margin = Thickness(16.,0.,16.,8.),
            backgroundColor = Color.White,
            cornerRadius = cornerRadius,
            content = View.Grid(
                coldefs=[Auto;Star],
                children=[
                    View.Frame(
                        height = 56.,
                        width = 64.,
                        padding = Thickness 0.,
                        isClippedToBounds = true,
                        cornerRadius = 8.,
                        margin = Thickness(-8., 0.,8.,0.),
                        content = View.Image(source = image,
                            width = 32.,
                            height = 32.,
                            aspect = Aspect.Fill)
                    )
                    (titleAndDescription title cardTitleFontSize description).Column(1)
                    ]
            ),
            hasShadow = true)

    let thingsTodo city margin =
        View.StackLayout(
            orientation = StackOrientation.Vertical,
            margin = margin,
            children = [
                (titleLabel "Things to do" titleFontSize).Row(0)
                (activityFrame "Classic landmarks" "Classic landmarks you need to see. Check these of your bucketlist!" (Path "ZurichLandmark")).Row(1)
                (activityFrame "Interesting sights" "Amazing sights that will make for stunning photos!" (Path "Sight")).Row(2)
                (activityFrame "Restaurants" "Where to go when you're feeling peckish and want to try out the local food." (Path "Restaurant")).Row(3)
                ]
        )

    let view (model: Model) dispatch =
        View.ContentPage(
            backgroundColor = backgroundColor,
            content = View.ScrollView(
                content = View.Grid(
                    rowdefs = [Auto; Auto; Star],
                    margin = Thickness(16.,8.,16.,-6.),
                    children = [
                        View.Grid(
                            coldefs = [Star; Auto],
                            children = [
                                (titleLabel "Destinations" titleFontSize).Column(0)
                                (materialButton magnify backgroundColor secondaryTextColor (fun() -> ())
                                    |> fun(button) -> button.WidthRequest 42.).Column(1)
                            ])
                        // todo: fix carousel view
                        View.CarouselView(
                            items = (model.Cities |> Seq.map (fun m -> cityDescriptionFrame m dispatch) |> Seq.toList),
                            height = 380.
                                ).Row(1)
                        (thingsTodo model.CurrentCity (Thickness(0., 32., 0., 0.))).Row(2)
                    ]
                    ))
        )

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> XamarinFormsProgram.run app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/tools.html for further  instructions.
    //
    do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/models.html for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif

