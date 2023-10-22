using AirApp.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirApp.Services
{
    public class EnvironmentStatusService 
    {
        HttpClient client;

        public string Authority
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                                     "https://login.microsoftonline.com/", "7bd81e6c-1bf4-4314-9b19-794e3334c5bf");
            }
        }

        private void addAuth()
        {
            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create("a8740e2d-19ac-495a-b97c-2af1c62bf45c")
                .WithClientSecret("hw48Q~s5QzPTZIlZblFs6GIlDDotSkS~JNLO1avf")
                .WithAuthority(new Uri(Authority))
                .Build();

            string[] ResourceIds = new string[] { "api://9eed7993-d8dc-452c-b09d-6b98f5ac1975/.default" };
            Task.Run(async () =>
            {
                AuthenticationResult result = null;
                try
                {
                    result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Token acquired \n");
                    Console.WriteLine(result.AccessToken);
                    Console.ResetColor();
                }
                catch (MsalClientException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            });
        }

        private static string BASE_URL { get; set; } = "https://losy-backend-dev-b1-plan.azurewebsites.net/api/";

        public EnvironmentStatusService()
        {
            client = new HttpClient();
        }

        public async Task<ObservableCollection<EnvironmentStatus>> LoadEnvironmentStatuses()
        {
            var statuses = new ObservableCollection<EnvironmentStatus>();

            var result = string.Format($"{BASE_URL}EnvironmentStatus", string.Empty);
            if (Temp.SortOnHumidity.HasValue && Temp.SortOnHumidity.Value)
            {
                result += "?Sorts=AirHumidity";
            }
            else if(Temp.SortOnHumidity.HasValue && !Temp.SortOnHumidity.Value)
            {
                result += "?Sorts=-AirHumidity";
            }

            Uri uri = new Uri(result);

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    statuses = JsonSerializer.Deserialize<ObservableCollection<EnvironmentStatus>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while fetching from uri --> [{uri}] \nException message --> [{ex.Message}]");
            }

            return statuses;
        }
    }
}
