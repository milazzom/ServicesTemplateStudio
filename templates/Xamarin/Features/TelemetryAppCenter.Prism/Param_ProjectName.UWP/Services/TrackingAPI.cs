using System;
using System.Collections.Generic;
using Param_ItemNamespace.Services;
using Microsoft.AppCenter.Analytics;

[assembly: Xamarin.Forms.Dependency(typeof(TrackingService))]

namespace Param_ItemNamespace.UWP.Services
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
            Analytics.TrackEvent($"Exception:{exception.Message}", eventProperties);
        }
    }
}
