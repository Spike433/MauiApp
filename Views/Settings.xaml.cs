using AirApp.Models;
using Controls.UserDialogs.Maui;
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

    private async void OnApplyFiltersClicked(object sender, EventArgs e)
    {
        DateTime startTDate = startDatePicker.Date + startTimePicker.Time;
        DateTime endTDate = endDatePicker.Date + endTimePicker.Time;

        var startUtc = startTDate.ToUniversalTime();
        var endUtc = endTDate.ToUniversalTime();

        // Combine the date and time to create a single DateTime


        string BASE_URL = "https://losy-backend-dev-b1-plan.azurewebsites.net/api/";
        var result = string.Format($"{BASE_URL}/QrCode/1?lockerName=3" +
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
                Stream imageStream = await response.Content.ReadAsStreamAsync();

                var imageSource = ImageSource.FromStream(() => imageStream);
                qr.Source = imageSource;
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
}