using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Param_ItemNamespace.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Param_ItemNamespace.ViewModels
{
	public class HockeyAppViewViewModel : ViewModelBase
    {
        public Boolean TrackEventToggle { get; set; }
        public Boolean TrackDependencyToggle { get; set; }
        public Boolean TrackExceptionToggle { get; set; }
        public Boolean TrackMetricToggle { get; set; }
        public Boolean TrackPageToggle { get; set; }
        public Boolean TrackedTimeEventToggle { get; set; }
        public Boolean TrackAggregatedMetricToggle { get; set; }
        public int MetricLoggingDelay { get; set; }
        public  string LoggingLevelItem { get; set; }

        public DelegateCommand TelemetryCommand { get; private set; }
        public HockeyAppViewViewModel(INavigationService navigationService) : base(navigationService)
        {
            TelemetryCommand = new DelegateCommand(OnTelemetryCommand);
            TrackEventToggle = true;
            TrackDependencyToggle = true;
            TrackExceptionToggle = true;
            TrackMetricToggle = true;
            TrackPageToggle = true;
            TrackedTimeEventToggle = true;
            TrackAggregatedMetricToggle = false;
            MetricLoggingDelay = TrackingService.AggregateMetricLoggingDelay;
            LoggingLevelItem = Enum.GetName(typeof(TrackingLoggingLevel), TrackingService.AppLoggingLevel);
        }

        private void OnTelemetryCommand()
        {
            TrackingService.AggregateMetricLoggingDelay = MetricLoggingDelay;
            TrackingService.AppLoggingLevel = (TrackingLoggingLevel)Enum.Parse(typeof(TrackingLoggingLevel), LoggingLevelItem);
 
            //TrackEvent
            if (TrackEventToggle)
            {
                DoTrackEvent(TrackingService);
            }

            //TrackDependency
            if (TrackDependencyToggle)
            {
                DoTrackDependency(TrackingService);
            }

            //TrackException
            if (TrackExceptionToggle)
            {
                DoTrackException(TrackingService);
            }

            //TrackMetric
            if (TrackMetricToggle)
            {
                DoTrackMetric(TrackingService);
            }
            //TrackPage
            if (TrackPageToggle)
            {
                DoTrackPage(TrackingService);
            }

            //Using 
            if (TrackedTimeEventToggle)
            {
                System.Diagnostics.Debug.WriteLine("Track Timed Event");
                using (new TrackedTimedEvent("DoStuff", TrackingService))
                {
                    var rand = new Random();
                    int delay = rand.Next(100, 500);
                    Task.Delay(delay).Wait();
                    /* Do stuff */
                }
            }

            //TrackAggregateMetric
            if (TrackAggregatedMetricToggle)
            {
                DoTrackAggregateMetricAsync(TrackingService);
            }
        }

        private void DoTrackPage(ITrackingService trackingService)
        {
            System.Diagnostics.Debug.WriteLine("Track Page");
            trackingService.TrackPageView("DoTrackPageView method");
        }

        private async void DoTrackAggregateMetricAsync(ITrackingService trackingService)
        {
            System.Diagnostics.Debug.WriteLine("Track Aggregate Metric - Started");

            var rand = new Random();
            int i = 0;
            for (i = 0; i < 1000; i++)
            {
                int delay = rand.Next(100, 500);
                double randomVal = rand.NextDouble();

                await Task.Delay(delay);

                trackingService.TrackAggregateMetric("My Fast Metric", delay, null, TimeSpan.FromMilliseconds(delay));

                var parameters = new Dictionary<string, string>
                {
                    {"delay", delay.ToString() },
                    {"double", randomVal.ToString() }
                };

                trackingService.TrackAggregateMetric("My Fast Double Metric", randomVal, parameters, TimeSpan.FromMilliseconds(delay), TrackingLoggingLevel.Information);
            }
            System.Diagnostics.Debug.WriteLine("Track Aggregate Metric - Completed");
        }

        private void DoTrackMetric(ITrackingService trackingService)
        {
            var rand = new Random();
            System.Diagnostics.Debug.WriteLine("Track Metric");
            trackingService.TrackMetric("DoTrackMetric", rand.NextDouble(), null);

            var parameters = new Dictionary<string, string>
            {
                {"param1", "value1" },
                {"param2", "value2" }
            };
            trackingService.TrackMetric("DoTrackMetric method with parameters - AppLoggingLevel is verbose", rand.NextDouble(), parameters);
        }

        private void DoTrackException(ITrackingService trackingService)
        {
            System.Diagnostics.Debug.WriteLine("Track Exception");
            try
            {
                throw new Exception("Test Exception");
            }
            catch (Exception e) // handled exception
            {
                System.Diagnostics.Debug.WriteLine($"DoTrackException {e.Message}");
                trackingService.TrackException(e, new Dictionary<string, string>());
            }

            try
            {
                throw new Exception("Test Exception with Parameters");
            }
            catch (Exception e) // handled exception
            {
                System.Diagnostics.Debug.WriteLine($"DoTrackException {e.Message}");
                var parameters = new Dictionary<string, string>()
                {
                    {"Param1", "Value1" },
                    {"Param2", "Value2" }
                };
                trackingService.TrackException(e, parameters);
            }
        }

        private void DoTrackDependency(ITrackingService trackingService)
        {
            System.Diagnostics.Debug.WriteLine("Track Dependency");
            var rand = new Random();
            int delay = rand.Next(100, 1000);
            trackingService.TrackDependency("DoTrackDependency method", "DoTrackDependency", DateTime.Now, TimeSpan.FromMilliseconds(delay), true);
        }

        private void DoTrackEvent(ITrackingService trackingService)
        {
            System.Diagnostics.Debug.WriteLine("Track Event");
            trackingService.TrackEvent("DoTrackEvent method");
        }
    }
}