using AirApp.Models;
using Controls.UserDialogs.Maui;
using Microsoft.Maui.Controls;
using System.Net;

namespace AirApp.Views;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();        
    }

    private void OnSortToggled(object sender, ToggledEventArgs e)
    {
		bool sortValue = e.Value;
        Temp.SortOnHumidity = sortValue;
    }

    private ImageSource qrImageSource;

    private async void OnApplyFiltersClicked(object sender, EventArgs e)
    {
        DateTime startTDate = startDatePicker.Date + startTimePicker.Time;
        DateTime endTDate = endDatePicker.Date + endTimePicker.Time;

        var startUtc = startTDate.ToUniversalTime();
        var endUtc = endTDate.ToUniversalTime();

        if (!int.TryParse(serial.Text, out int serialNumber) || !int.TryParse(locker.Text, out int lockerNumber))
        {
            UserDialogs.Instance.Alert("Serial Or Locker Number is not a number.");
            return;
        }

        string BASE_URL = "https://losy-backend-dev-b1-plan.azurewebsites.net/api/";
        var result = string.Format($"{BASE_URL}/QrCode/{serialNumber}?lockerName={lockerNumber}" +
            $"&reservationStart={startUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}" +
            $"&reservationEnd={endUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}" +
            $"&userId=0", string.Empty);

        Uri uri = new Uri(result);
        var client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(result);
            if (response.IsSuccessStatusCode)
            {
                var imageStream = await response.Content.ReadAsStreamAsync();

                // Create an ImageSource from the stream and set it to the qrImageSource.
                qrImageSource = ImageSource.FromStream(() => imageStream);
                qr.Source = qrImageSource;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(errorMessage);

                // Display the error message to the user.
                UserDialogs.Instance.Alert(errorMessage);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Check if qrImageSource is not null before setting the source to qr.
        if (qrImageSource != null)
        {
            qr.Source = qrImageSource;
        }
    }
}