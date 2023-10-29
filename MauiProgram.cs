using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Controls.UserDialogs.Maui;

namespace AirApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                 .UseUserDialogs(() =>
                 {
                     //setup your default styles for dialogs
                     AlertConfig.DefaultBackgroundColor = Colors.Grey;
                     AlertConfig.DefaultMessageColor = Colors.WhiteSmoke;

                     ToastConfig.DefaultCornerRadius = 15;
                 })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMicrocharts();

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}