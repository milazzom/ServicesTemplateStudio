using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Services.BotTemplates.Param_ProjectName.Schema;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ProjectName.Services
{
    public static class FakeRestaurants
    {
        public static List<Restaurant> GetRestaurants()
        {
            var restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(File.ReadAllText("Services\\Data\\Restaurants.json"));
            return restaurants;
        }

        public static List<Restaurant> GetRestaurants(string cityName)
        {
            return GetRestaurants().Where(r => r.Location == cityName).ToList();
        }
    }
}