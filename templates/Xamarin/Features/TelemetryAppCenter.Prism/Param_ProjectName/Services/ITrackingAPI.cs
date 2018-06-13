using System;
using System.Collections.Generic;

namespace Param_ItemNamespace.Services
{
    public interface ITrackingAPI
    {
        /// <summary>
        /// Track Events with Properties
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        void TrackEvent(string eventName, IDictionary<string, string> properties);

        /// <summary>
        /// Track events
        /// </summary>
        /// <param name="eventName"></param>
        void TrackEvent(string eventName);

        /// <summary>
        /// Track Exceptions 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="eventProperties"></param>
        void TrackException(Exception exception, IDictionary<string, string> eventProperties);
    }
}