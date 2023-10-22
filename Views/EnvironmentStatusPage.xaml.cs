using AirApp.Models;
using Controls.UserDialogs.Maui;

namespace AirApp.Views;

public partial class EnvironmentStatusPage : ContentPage
{
	public EnvironmentStatusPage()
	{
        InitializeComponent();
    }

    private async void environmentCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
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

    protected override void OnDisappearing()
    {
        // Clear the binding context when the page is disappearing
        BindingContext = null;
        base.OnDisappearing();
    }

    protected override void OnAppearing()
    {
        BindingContext = new Models.AllEnvironmentStatuses();

        base.OnAppearing();
    }

}