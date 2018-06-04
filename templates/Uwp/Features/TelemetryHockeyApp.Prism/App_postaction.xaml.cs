using Windows.UI.Xaml;
//^^
//{[{
using Param_RootNamespace.Services;
using Microsoft.HockeyApp;
//}]}

namespace Param_RootNamespace
{
    public sealed partial class App : PrismUnityApplication
    {
//{[{        
        private IFrameFacade frameFacade;
        private ITrackingService trackingService;
        private readonly string HockeyAppId = string.Empty; // TODO - add Hockey App ID

//}]}
        public App()
        {
            InitializeComponent();
        }
//{[{

        protected override INavigationService OnCreateNavigationService(IFrameFacade rootFrame)
        {
            frameFacade = rootFrame;
            frameFacade.NavigatingFrom += FrameFacade_NavigatedFrom;
            return base.OnCreateNavigationService(rootFrame);
        }

        private void FrameFacade_NavigatedFrom(object sender, NavigatingFromEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Navigated From: {e.SourcePageType}");
            trackingService.TrackPageView(e.SourcePageType.ToString());
        }

        private void InitializeTelemetry()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                HockeyAppId = string.Empty; // disable sending telemetry to HockeyApp/AppInsights
            }
#endif
            if (!string.IsNullOrEmpty(HockeyAppId))
            {
                TelemetryConfiguration telemetryConfig = new TelemetryConfiguration
                {
                    EnableDiagnostics = true,
                    Collectors = WindowsCollectors.Metadata | WindowsCollectors.Session | WindowsCollectors.UnhandledException
                };
                HockeyClient.Current.Configure(HockeyAppId, telemetryConfig);
            }
            trackingService = Current.Container.Resolve<ITrackingService>();
        }
//}]}
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
//{[{
            // Telemetry Service
            Container.RegisterType<ITrackingService, TrackingServiceHockeyApp>(new ContainerControlledLifetimeManager());
//}]}
        }

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
//{[{
            InitializeTelemetry();
//}]}
        }
    }
}
