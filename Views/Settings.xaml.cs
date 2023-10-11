namespace AirApp.Views;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

    private void OnSortToggled(object sender, ToggledEventArgs e)
    {
		bool isSorted = e.Value;
    }

    private void OnApplyFiltersClicked(object sender, EventArgs e)
    {

    }
}