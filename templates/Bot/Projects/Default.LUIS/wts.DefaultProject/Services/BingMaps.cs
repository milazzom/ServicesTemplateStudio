using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Microsoft.Services.BotTemplates.Param_ProjectName.Schema;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ProjectName.Services
{
    public class BingMaps
    {
        private readonly string _bingMapsKey;

        public BingMaps(string bingMapsKey)
        {
            _bingMapsKey = bingMapsKey;
        }

        public string GetState(double latitude, double longitude)
        {
            using (var client = new WebClient())
            {
                var queryString = $"https://dev.virtualearth.net/REST/v1/Locations/{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}?key={_bingMapsKey}";
                var response = client.DownloadString(queryString);
                var bingMapsResponse = JsonConvert.DeserializeObject<BingMapsResponse>(response);
                return bingMapsResponse.resourceSets.First()?.resources.First().address.adminDistrict;
            }
        }
    }
}