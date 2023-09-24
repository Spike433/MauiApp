using AirApp.Models;

namespace AirApp.Views;

public partial class EnvironmentStatusPage : ContentPage
{
	public EnvironmentStatusPage()
	{
        InitializeComponent();
		BindingContext = new Models.AllEnvironmentStatuses();
	}

    private async void environmentCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if(e.CurrentSelection.Count != 0)
		{
			var status = e.CurrentSelection.FirstOrDefault() as EnvironmentStatus;
			var navParams = new Dictionary<string, object>()
			{
				{"status", status}
			};

			await Shell.Current.GoToAsync("StatusDetails", navParams);
		}

		//clear selected item
        environmentStatusesCollection.SelectedItem = null;
    }    
}