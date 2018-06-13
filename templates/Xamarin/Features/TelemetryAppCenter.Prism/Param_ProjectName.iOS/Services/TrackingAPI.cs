using System;
using System.Collections.Generic;
using Param_ItemNamespace.Services;
using System.ComponentModel;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: Xamarin.Forms.Dependency(typeof(TrackingService))]

namespace Param_ItemNamespace.iOS.Services
{
    internal class TrackingAPI : ITrackingAPI
    {
        public void TrackEvent(string eventName, IDictionary<string, string> properties)
        {
            Analytics.TrackEvent(eventName, properties);
        }

        public void TrackEvent(string eventName)
        {
            Analytics.TrackEvent(eventName);
        }

        public void TrackException(Exception exception, IDictionary<string, string> eventProperties)
        {
            Crashes.TrackError(exception, eventProperties);
        }
    }
}
