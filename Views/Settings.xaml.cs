using AirApp.Models;

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

    private void OnApplyFiltersClicked(object sender, EventArgs e)
    {
        Temp.Start = startDatePicker.Date;
        Temp.End = endDatePicker.Date;

    }
}