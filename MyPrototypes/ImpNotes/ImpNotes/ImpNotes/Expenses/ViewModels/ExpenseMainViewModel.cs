
using System;
using System.Windows.Input;
using ImpNotes.Notes.ViewModels;
using ImpNotes.Notes.Views;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

//using Entry = Microcharts.Chart;

namespace ImpNotes.Expenses.ViewModels
{
    public class ExpenseMainViewModel
    {
        public ICommand GoToConceptsCommand => new Command(this.GoToConcepts);

        private async void GoToConcepts()
        {
            //await (App.Current.MainPage as Xamarin.Forms.Shell)
            //    .GoToAsync($"app:///ImpNotes/concepts");
            await (App.Current.MainPage as Shell)
                .GoToAsync("app://ImpNotes/Expenses/Views/ConceptPage");
            //await (App.Current.MainPage as Xamarin.Forms.Shell)
            //    .GoToAsync("concepts");
            //   await Shell.CurrentShell.GoToAsync($"app:///ImpNotes/concepts",true);
          //  await Shell.CurrentShell.GoToAsync($"app:///ps.net/concepts", true);
          //  await Shell.CurrentShell.GoToAsync($"app:///home/tabs/favorites/myfavorites?text={id}", true);
          //  Shell.CurrentShell.FlyoutIsPresented = false;
            //await Xamarin.Forms.Shell.Current.GoToAsync("//animals/monkeys");
            //await Shell.Current.GoToAsync("monkeydetails");

        }



        public ConceptsViewModel Concept { get; set; }

        public Chart IncomeAndExpenses => new DonutChart()
        {
            Entries = new[]
            {

                new Entry(60)
                {
                    Color = SKColor.Parse("#ff80ff"),
                    TextColor = SKColor.Parse("#ff80ff"),
                    Label = "Dog",
                    ValueLabel = "60%"
                },
                new Entry(40)
                {
                    Color = SKColor.Parse("#A8F4B4"),
                    TextColor = SKColor.Parse("#A8F4B4"),
                    Label = "Cat",
                    ValueLabel = "40%"
                },
            },
            LabelTextSize = 45,
            HoleRadius = 0.20f,
        };

        //public Chart BarChartSample => new BarChart()
        //{
        //    Entries = new[]
        //     {
        //        new Entry(100)
        //        {
        //            Color = SKColor.Parse("#ff80ff"),
        //            TextColor = SKColor.Parse("#ff80ff"),
        //            Label = "Baseball",
        //            ValueLabel = "100%"
        //        } ,
        //        new Entry(75)
        //        {
        //            Color = SKColor.Parse("#A8F4B4"),
        //            TextColor = SKColor.Parse("#A8F4B4"),
        //            Label = "Volleyball",
        //            ValueLabel = "75%"
        //        },
        //        new Entry(25)
        //        {
        //            Color = SKColor.Parse("#B7A8F4"),
        //            TextColor = SKColor.Parse("#B7A8F4"),
        //            Label = "Tennis",
        //            ValueLabel = "25%"
        //        },
        //    },
        //    LabelTextSize = 45,
        // //   LabelOrientation = Orientation.Vertical
        //};

        //public Chart IncomeAndExpenses => new LineChart()
        //{

        //    Entries = new[]
        //    {
        //        new Entry(100)
        //        {
        //            Color = SKColor.Parse("#ff80ff"),
        //            TextColor = SKColor.Parse("#ff80ff"),
        //            Label = "Rice",
        //            ValueLabel = "100%"
        //        },
        //        new Entry(75)
        //        {
        //            Color = SKColor.Parse("#A8F4B4"),
        //            TextColor = SKColor.Parse("#A8F4B4"),
        //            Label = "Chicken",
        //            ValueLabel = "75%"
        //        },
        //        new Entry(25)
        //        {
        //            Color = SKColor.Parse("#B7A8F4"),
        //            TextColor = SKColor.Parse("#B7A8F4"),
        //            Label = "Beans",
        //            ValueLabel = "25%"
        //        },
        //        new Entry(5)
        //        {
        //            Color = SKColor.Parse("#1ab2ff"),
        //            TextColor = SKColor.Parse("#1ab2ff"),
        //            Label = "Onions",
        //            ValueLabel = "5%"
        //        },
        //    },
        //   // AnimationDuration = new TimeSpan(9000),
        //  //  AnimationProgress = 6000,
        //    LineMode = LineMode.Straight,
        //    LabelTextSize = 45,
        //  //  LabelOrientation = Orientation.Horizontal,
        //};

        //public Chart PointChartSample => new PointChart()
        //{
        //    Entries = new[]
        //    {
        //        new Entry(100)
        //        {
        //            Color = SKColor.Parse("#ff80ff"),
        //            TextColor = SKColor.Parse("#ff80ff"),
        //            Label = "Paper",
        //            ValueLabel = "100%"
        //        },
        //        new Entry(75)
        //        {
        //            Color = SKColor.Parse("#A8F4B4"),
        //            TextColor = SKColor.Parse("#A8F4B4"),
        //            Label = "Pencil",
        //            ValueLabel = "75%"
        //        },
        //        new Entry(25)
        //        {
        //            Color = SKColor.Parse("#B7A8F4"),
        //            TextColor = SKColor.Parse("#B7A8F4"),
        //            Label = "Sheet",
        //            ValueLabel = "25%"
        //        },
        //        new Entry(5)
        //        {
        //            Color = SKColor.Parse("#1ab2ff"),
        //            TextColor = SKColor.Parse("#1ab2ff"),
        //            Label = "Pen",
        //            ValueLabel = "5%"
        //        },
        //    },
        //   // AnimationDuration = new TimeSpan(6000),
        //    LabelTextSize = 45,
        //   // LabelOrientation = Orientation.Horizontal
        //};

        //public Chart RadielGaugeChartSample => new RadialGaugeChart()
        //{
        //    Entries = new[]
        //    {
        //        new Entry(100)
        //        {
        //            Color = SKColor.Parse("#ff80ff"),
        //            TextColor = SKColor.Parse("#ff80ff"),
        //            Label = "Software",
        //            ValueLabel = "100%"
        //        },
        //        new Entry(75)
        //        {
        //            Color = SKColor.Parse("#A8F4B4"),
        //            TextColor = SKColor.Parse("#A8F4B4"),
        //            Label = "Administration",
        //            ValueLabel = "75%",
        //        },
        //        new Entry(25)
        //        {
        //            Color = SKColor.Parse("#B7A8F4"),
        //            TextColor = SKColor.Parse("#B7A8F4"),
        //            Label = "Marketing",
        //            ValueLabel = "25%"
        //        },
        //        new Entry(5)
        //        {
        //            Color = SKColor.Parse("#1ab2ff"),
        //            TextColor = SKColor.Parse("#1ab2ff"),
        //            Label = "Languages",
        //            ValueLabel = "5%"
        //        },
        //    },
        //    LineSize = 15,
        //   // AnimationDuration = new TimeSpan(6000),
        //    LabelTextSize = 45,
        //};

        //public Chart DonutChartSample => new DonutChart()
        //{
        //    Entries = new[]
        //   {
        //        new Entry(100)
        //        {
        //            Color = SKColor.Parse("#ff80ff"),
        //            TextColor = SKColor.Parse("#ff80ff"),
        //            Label = "Dog",
        //            ValueLabel = "100%"
        //        },
        //        new Entry(75)
        //        {
        //            Color = SKColor.Parse("#A8F4B4"),
        //            TextColor = SKColor.Parse("#A8F4B4"),
        //            Label = "Cat",
        //            ValueLabel = "75%"
        //        },
        //        new Entry(25)
        //        {
        //            Color = SKColor.Parse("#B7A8F4"),
        //            TextColor = SKColor.Parse("#B7A8F4"),
        //            Label = "Rabbit",
        //            ValueLabel = "25%"
        //        },
        //        new Entry(5)
        //        {
        //            Color = SKColor.Parse("#1ab2ff"),
        //            TextColor = SKColor.Parse("#1ab2ff"),
        //            Label = "Octopus",
        //            ValueLabel = "5%"
        //        },
        //    },
        //    LabelTextSize = 45,
        //    HoleRadius = 0.20f,
        //};

        //public Chart RadarChartSample => new RadarChart()
        //{
        //    Entries = new[]
        //    {
        //        new Entry(100)
        //        {
        //            Color = SKColor.Parse("#ff80ff"),
        //            TextColor = SKColor.Parse("#ff80ff"),
        //            Label = "Banana",
        //            ValueLabel = "100%"
        //        },
        //        new Entry(75)
        //        {
        //            Color = SKColor.Parse("#A8F4B4"),
        //            TextColor = SKColor.Parse("#A8F4B4"),
        //            Label = "Orange",
        //            ValueLabel = "75%"
        //        },
        //        new Entry(25)
        //        {
        //            Color = SKColor.Parse("#B7A8F4"),
        //            TextColor = SKColor.Parse("#B7A8F4"),
        //            Label = "Apple",
        //            ValueLabel = "25%"
        //        },
        //        new Entry(5)
        //        {
        //            Color = SKColor.Parse("#1ab2ff"),
        //            TextColor = SKColor.Parse("#1ab2ff"),
        //            Label = "Cherry",
        //            ValueLabel = "5%"
        //        },
        //    },
        //    LabelTextSize = 45
        //};
    }



}
