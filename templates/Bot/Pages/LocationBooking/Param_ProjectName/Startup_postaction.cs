using Microsoft.Extensions.DependencyInjection;
//{[{
using Microsoft.Services.BotTemplates.Param_ProjectName.Services;
using Microsoft.Services.BotTemplates.Param_ProjectName.Common.Telemetry;
//}]}

namespace Microsoft.Services.BotTemplates.Param_ProjectName
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register LUIS recognizer service
            var luisRecognizer = CreateLuisRecognizer();
            services.AddSingleton(luisRecognizer);
//{[{

            // Register BingMaps service
            var bingMaps = new BingMaps(Configuration.GetSection("BingMapsKey").Value);
            services.AddSingleton(bingMaps);
//}]}
        }
    }
}