using AirApp.Views;

namespace AirApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("StatusDetails", typeof(StatusDetails));
        }
    }
}