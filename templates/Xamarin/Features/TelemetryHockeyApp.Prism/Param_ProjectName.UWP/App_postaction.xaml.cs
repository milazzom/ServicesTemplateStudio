using Windows.UI.Xaml;
//^^
//{[{
using Microsoft.HockeyApp;
//}]}

namespace Param_RootNamespace.UWP
{
    sealed partial class App : Application
    {
//{[{
        const string HockeyAppId = ""; //TODO: Replace with HockeyApp ID

//}]}
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
//{[{
            InitialIzeTelemetry();
//}]}
        }
 //{[{
        private void InitialIzeTelemetry()
        {
            if (!string.IsNullOrEmpty(HockeyAppId))
            {
                TelemetryConfiguration telemetryConfig = new TelemetryConfiguration
                {
                    EnableDiagnostics = true,
                    Collectors = WindowsCollectors.Metadata | WindowsCollectors.Session | WindowsCollectors.UnhandledException
                };

                HockeyClient.Current.Configure(HockeyAppId, telemetryConfig);
            }
        }
//}]}
    }
}
