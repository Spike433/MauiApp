using AirApp.Models;
using Controls.UserDialogs.Maui;
using Microcharts;
using SkiaSharp;
using static Android.Security.Identity.CredentialDataResult;

namespace AirApp.Views;

public partial class Home : ContentPage
{
    ChartEntry[] entries = new[]
    {
        new ChartEntry(212)
        {
            Label="UWP",
            ValueLabel="112",
            Color=SKColor.Parse("#BDBDFD"),
            ValueLabelColor = SKColor.Parse("#f5f5f5")
        },
        new ChartEntry(248)
        {
            Label = "Andorid",
            ValueLabel = "214",
            Color = SKColor.Parse("#6528F7"),
            ValueLabelColor = SKColor.Parse("#f5f5f5")
        },
         new ChartEntry(130)
        {
            Label = "test 2",
            ValueLabel = "214",
            Color = SKColor.Parse("#8E8FFA"),
            ValueLabelColor = SKColor.Parse("#f5f5f5")
        },
        new ChartEntry(100)
        {
            Label = "test",
            ValueLabel = "214",
            Color = SKColor.Parse("#535EEB"),
            ValueLabelColor = SKColor.Parse("#f5f5f5")
        },
    };
    public Home()
	{
		InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        //_ = new Models.AllEnvironmentStatuses();

        var donutEntry = Array.Empty<ChartEntry>();

        var grouped =  Temp.EnvironmentStatuses?.GroupBy(d => d.Uuid).ToList();

        myChart.Chart = new DonutChart
        {
            Entries = entries,
            //ValueLabelOrientation = Orientation.Horizontal,
            LabelTextSize = 30,
            BackgroundColor = SKColor.Parse("#424242"),
            LabelColor = SKColor.Parse("#f5f5f5"),
        };

        second.Chart = new BarChart
        {
            Entries = entries,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelTextSize = 30,
            BackgroundColor = SKColor.Parse("#424242"),
            LabelColor = SKColor.Parse("#f5f5f5"),
        };

        base.OnAppearing();
    }

}