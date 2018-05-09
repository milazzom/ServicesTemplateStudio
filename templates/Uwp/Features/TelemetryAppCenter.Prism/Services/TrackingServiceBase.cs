using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;

namespace Param_ItemNamespace.Services
{
    public class TrackingServiceBase
    {
        public LoggingLevel AppLoggingLevel { get; set; } = LoggingLevel.Information;
        public LoggingLevel TrackEventLogLevel { get; set; } = LoggingLevel.Information;
        public LoggingLevel TrackDependencyLogLevel { get; set; } = LoggingLevel.Information;
        public LoggingLevel TrackPageLogLevel { get; set; } = LoggingLevel.Information;
        public LoggingLevel TrackExceptionLogLevel { get; set; } = LoggingLevel.Error;
        public LoggingLevel TrackMetricLogLevel { get; set; } = LoggingLevel.Warning;
        public int MetricLoggingDelay { get; set; } = 60000;
        protected readonly Dictionary<string, TrackingServiceMetric> TrackingServiceMetricCollection = new Dictionary<string, TrackingServiceMetric>();
        public BackgroundWorker thread;
        protected readonly Boolean stop = false;
        protected readonly object _lockObject = new object();
    }

    public class TrackingServiceMetric
    {
        private DateTime _Start;
        public List<double> Measurements = new List<double>();

        public DateTime Start
        {
            get { return _Start; }
            set
            {
                _Start = value;
                Measurements.Clear();
            }
        }

        public double Value
        {
            set
            {
                Measurements.Add(value);
            }
        }

        public IDictionary<string, string> MetricProperties { get; set; }
        public TimeSpan MetricTimeSpan { get; set; } = TimeSpan.FromSeconds(60);
        public LoggingLevel MetricLoggingLevel { get; set; } = LoggingLevel.Warning;
    }

    /// <summary>
    /// A struct for measuring latency of operations.
    /// </summary>
    /// <remarks>
    /// Using struct here to prevent impact on the GC. Pass by value is fine as
    /// the only behavior occurs on dispose. Multiple calls to dispose will
    /// result in multiple latency metrics being tracked, so it is suggested
    /// to use this helper in the context of a <code>using</code> block, e.g.:
    /// 
    /// <code>
    /// using (new TrackedTimedEvent("MyMetric"))
    /// {
    ///    /* Do stuff */
    /// }
    /// </code>
    /// 
    /// </remarks>
    internal struct TrackedTimedEvent : IDisposable
    {
        private static readonly Stopwatch s_stopwatch = Stopwatch.StartNew();

        private readonly string _eventName;
        private readonly long _startTicks;
        private readonly DateTime _startTime;
        private readonly ITrackingService _trackingService;
        private readonly string _commandName;

        public TrackedTimedEvent(string eventName, string commandName, ITrackingService trackingService)
        {
            _eventName = eventName;
            _startTicks = s_stopwatch.ElapsedTicks;
            _startTime = DateTime.UtcNow;
            _trackingService = trackingService;
            _commandName = commandName;
        }

        public void Dispose()
        {
            s_stopwatch.Stop();
            _trackingService.TrackDependency(_eventName, _commandName, _startTime, s_stopwatch.Elapsed, true);
        }
    }
}
