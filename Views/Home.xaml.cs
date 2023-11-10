using Android.Renderscripts;
using Controls.UserDialogs.Maui;
using Microcharts;
using SkiaSharp;

namespace AirApp.Views;

public partial class Home : ContentPage
{
    SKColor[] palette = new SKColor[]
    {
        SKColor.Parse("#2c3e50"),
        SKColor.Parse("#3498db"),
        SKColor.Parse("#e74c3c"),
        SKColor.Parse("#2ecc71"),
        SKColor.Parse("#f1c40f"),
        SKColor.Parse("#e67e22"),
        SKColor.Parse("#1abc9c"),
        SKColor.Parse("#8e44ad"),
    };

    List<ChartEntry> entries = new List<ChartEntry>();
    List<ChartEntry> line = new List<ChartEntry>();

    public Home()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        Random random = new Random();
        entries.Clear();
        line.Clear();
        var current = new DateTime(2023, 6, 1);

        for (int value = 0; value <= 7; value++)
        {
            int randomValue = random.Next(18,25);
            int randomHumidity = random.Next(10,50);
            int randomColor = random.Next(0,palette.Length-1);
            var randomPickColor = palette.Length - 1 - randomColor;

            ChartEntry entry = new ChartEntry(randomValue)
            {
                Label = current.ToString("dd/MM"),
                ValueLabel = randomValue.ToString()+ "°C",
                Color = palette[randomPickColor], // Use a color from the palette based on the value
                ValueLabelColor = palette[randomPickColor], // Use the same color for the value label
            };

            var pressure = new ChartEntry(randomHumidity)
            {
                Label = current.ToString("dd/MM"),
                ValueLabel = randomHumidity.ToString()+"%",
                Color = palette[randomPickColor], // Use a color from the palette based on the value
                ValueLabelColor = palette[randomPickColor], // Use the same color for the value label
            };

            current = current.AddDays(1);

            entries.Add(pressure);
            line.Add(entry);
        }

        myChart.Chart = new BarChart
        {
            Entries = entries,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelTextSize = 40,
            BackgroundColor = SKColor.Parse("#424242"),
            LabelColor = SKColor.Parse("#3498db"),
        };

        lineChart.Chart = new LineChart
        {
            ValueLabelOrientation = Orientation.Horizontal,
            Entries = line,
            LabelTextSize = 40,
            BackgroundColor = SKColor.Parse("#424242"),
            LabelColor = SKColor.Parse("#3498db"),
        };
    }
}