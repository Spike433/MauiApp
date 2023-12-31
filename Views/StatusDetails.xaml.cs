using AirApp.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AirApp.Views;

[QueryProperty(nameof(EnvironmentStatus), "status")]
public partial class StatusDetails : ContentPage, IQueryAttributable, INotifyPropertyChanged
{
	public EnvironmentStatus status { get; set; }

	public StatusDetails()
	{
		InitializeComponent();
		BindingContext = this;

        Shell.SetTabBarIsVisible(this, false);

        Random random = new Random();
        int randomNumber = random.Next(20, 26); // Generates numbers from 17 to 25 (inclusive)

        // Set the generated number as the text of the Label
        cloudinessTxt.Text = randomNumber.ToString() + "%";
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        status = query[nameof(status)] as EnvironmentStatus;
        FormattedIndoorAirQuality = AddSpacesToCamelCase(status.IndoorAirQuality);
        OnPropertyChanged(nameof(status));
    }

    public string AddSpacesToCamelCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        // Use regular expression to insert spaces before capital letters
        string result = Regex.Replace(input, "(\\B[A-Z])", " $1");

        // Capitalize the first letter
        result = char.ToUpper(result[0]) + result.Substring(1);

        return result;
    }

    private string _formattedIndoorAirQuality;

    public string FormattedIndoorAirQuality
    {
        get => _formattedIndoorAirQuality;
        set
        {
            if (_formattedIndoorAirQuality != value)
            {
                _formattedIndoorAirQuality = value;
                OnPropertyChanged(nameof(FormattedIndoorAirQuality));
            }
        }
    }
}