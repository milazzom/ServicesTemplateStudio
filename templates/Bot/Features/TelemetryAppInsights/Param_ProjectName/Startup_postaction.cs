using Microsoft.Extensions.DependencyInjection;
//{[{
using Microsoft.Services.BotTemplates.Param_ProjectName.Services;
//}]}

namespace Microsoft.Services.BotTemplates.Param_ProjectName
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register LUIS recognizer service
            var luisRecognizer = CreateLuisRecognizer();
            services.AddSingleton(luisRecognizer);
//{[{
    
            // Initialize and register telemetrylogger
            var telemetryOptions = CreateTelemetryOptions(Configuration);
            var telemetryLogger = new TelemetryLogger(telemetryOptions);
            services.AddSingleton<ITelemetryLogger>(telemetryLogger);

//}]}
            services.AddBot<Param_ProjectName>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

                options.Middleware.Add(new CatchExceptionMiddleware<Exception>(async (context, exception) =>
                {
                    await context.TraceActivity("Param_ProjectName Exception", exception);
                    await context.SendActivity("Sorry, it looks like something went wrong!");
                }));
//{[{

                options.Middleware.Add(new ActivityTrackerMiddleware(telemetryLogger));
//}]}
            });
        }

//{[{
    
        private static TelemetryOptions CreateTelemetryOptions(IConfiguration configuration)
        {
            //TODO add configuration for:
            //ExtractKeyPhrases = true;
            //LogOriginalMessages = true;
            //LogUserName = true;
            //AnalyzeSentiment = true;
            //SentimentWordThreshold = 3;

            var telemetryOptions = new TelemetryOptions
            {
                BotId = configuration.GetSection("BotId").Value,
#if DEBUG
                LogActivityInfo = true,
                LogOriginalMessages = true,
                LogUserName = true,
#else
                LogActivityInfo = false,
                LogOriginalMessages = false,
                LogUserName = false,

#endif
                TelemetryStorageProvider = new AppInsightsTelemetryStorage(new TelemetryConfiguration(configuration.GetSection("ApplicationInsights:InstrumentationKey").Value))
            };
            return telemetryOptions;
        }
//}]}
    }  
}