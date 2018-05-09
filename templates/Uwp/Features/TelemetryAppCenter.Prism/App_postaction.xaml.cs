using Windows.UI.Xaml;
//^^
//{[{
using Param_RootNamespace.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
//}]}

namespace Param_RootNamespace
{
    public sealed partial class App : PrismUnityApplication
    {
//{[{        
        private IFrameFacade frameFacade;
        private ITrackingService trackingService;
        private readonly string AppCenterId = string.Empty; // TODO - add Hockey App ID

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
            trackingService.TrackEvent($"Navigated From: {e.SourcePageType}");
        }

        private void InitializeTelemetry()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                AppCenterId = string.Empty; // disable sending telemetry to HockeyApp/AppInsights
            }
#endif
            if (!string.IsNullOrEmpty(AppCenterId))
            {
                Microsoft.AppCenter.AppCenter.Start(AppCenterId, typeof(Analytics), typeof(Crashes));
            }
            trackingService = Current.Container.Resolve<ITrackingService>();
        }
//}]}
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
//{[{
            // Telemetry Service
            Container.RegisterType<ITrackingService, TrackingServiceAppCenter>(new ContainerControlledLifetimeManager());
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
