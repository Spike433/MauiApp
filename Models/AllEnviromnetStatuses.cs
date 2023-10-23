using AirApp.Services;
using Android.App;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirApp.Models
{
    public class AllEnvironmentStatuses
    {
        public ObservableCollection<EnvironmentStatus> EnvironmentStatuses { get; set; } = new ObservableCollection<EnvironmentStatus>();

        public AllEnvironmentStatuses() 
        { 
            LoadStatuses(); 
        }

        public async void LoadStatuses()
        {
            UserDialogs.Instance.Loading("Loading...");

            var repository = new EnvironmentStatusService();
            var statuses = await repository.LoadEnvironmentStatuses();

            foreach (var status in statuses) 
            {
                EnvironmentStatuses.Add(status);
                Temp.EnvironmentStatuses.Add(status);
            }

            UserDialogs.Instance.HideHud();
        }
    }
}
