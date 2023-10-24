using AirApp.Models;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net;
using System.Text.Json;

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
        Temp.Start = startDatePicker.Date;
        Temp.End = endDatePicker.Date;


        //try
        //{
        //    System.Net.WebRequest request =
        //        WebRequest.Create(uri);
        //    System.Net.WebResponse response = request.GetResponse();
        //    System.IO.Stream responseStream =
        //        response.GetResponseStream();
        //}
        //catch (System.Net.WebException web)
        //{
        //    await Console.Out.WriteLineAsync("Error");
        //}

        string BASE_URL = "https://losy-backend-dev-b1-plan.azurewebsites.net/api/";
        var result = string.Format($"{BASE_URL}/QrCode/1?lockerName=3" +
            $"&reservationStart=2023-10-28" +
            $"&reservationEnd=2023-10-29" +
            $"&userId=123", string.Empty);

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
            else if(response.StatusCode == HttpStatusCode.BadRequest) 
            {
                await Console.Out.WriteLineAsync(response.Content.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}