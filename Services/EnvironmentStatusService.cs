using AirApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirApp.Services
{
    public class EnvironmentStatusService 
    {
        HttpClient client;
        
        private static string BASE_URL { get; set; } = "https://losy-backend-dev-b1-plan.azurewebsites.net/api/";

        public EnvironmentStatusService()
        {
            client = new HttpClient();            
        }

        public async Task<ObservableCollection<EnvironmentStatus>> LoadEnvironmentStatuses()
        {
            var statuses = new ObservableCollection<EnvironmentStatus>();

            Uri uri = new Uri(string.Format($"{BASE_URL}EnvironmentStatus", string.Empty));
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
