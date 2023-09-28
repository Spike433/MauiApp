using Microcharts;
using SkiaSharp;

namespace AirApp.Views;

public partial class Home : ContentPage
{
    ChartEntry[] entries = new[]
    {
        new ChartEntry(212)
        {
            Label="UWP",
            ValueLabel="112",
            Color=SKColor.Parse("#2c3e50"),
            ValueLabelColor = SKColor.Parse("#3498db")
        },
        new ChartEntry(248)
        {
            Label = "Andorid",
            ValueLabel = "214",
            Color = SKColor.Parse("#3498db"),
            ValueLabelColor = SKColor.Parse("#3498db")            
        }
    };
    public Home()
	{
		InitializeComponent();

        myChart.Chart = new BarChart
        {
            Entries = entries,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelTextSize = 50,
            BackgroundColor = SKColor.Parse("#424242"),
            LabelColor = SKColor.Parse("#3498db"),                        
        };
	}	
}