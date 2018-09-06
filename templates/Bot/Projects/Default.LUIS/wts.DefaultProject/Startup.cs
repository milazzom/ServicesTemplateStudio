using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Ai.QnA;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Builder.TraceExtensions;
using Microsoft.Cognitive.LUIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.ContentModerator;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Common.AI.TextAnalytics;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Common.Telemetry;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Services;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject
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

            // Register QnAMaker service
            var qnaMaker = CreateQnAMaker();
            services.AddSingleton(qnaMaker);

            // Register BingMaps service
            var bingMaps = new BingMaps(Configuration.GetSection("BingMapsKey").Value);
            services.AddSingleton(bingMaps);
            
            // Initialize and register telemetrylogger
            var telemetryOptions = CreateTelemetryOptions(Configuration);
            var telemetryLogger = new TelemetryLogger(telemetryOptions);
            services.AddSingleton<ITelemetryLogger>(telemetryLogger);

            services.AddBot<wts.DefaultProject>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

                options.Middleware.Add(new CatchExceptionMiddleware<Exception>(async (context, exception) =>
                {
                    await context.TraceActivity("wts.DefaultProject Exception", exception);
                    await context.SendActivity("Sorry, it looks like something went wrong!");
                }));

                IStorage dataStore = new MemoryStorage();

                options.Middleware.Add(new ConversationState<Dictionary<string, object>>(dataStore));
                options.Middleware.Add(new TextAnalyticsMiddleware(CreateTextAnalyticsOptions()));
                options.Middleware.Add(new ActivityTrackerMiddleware(telemetryLogger));
                options.Middleware.Add(new ContentModeratorMiddleware(CreateContentModeratorOptions()));
            });
        }

        private LuisRecognizer CreateLuisRecognizer()
        {
            ILuisModel luisModel = new LuisModel(
                Configuration.GetSection("LUIS:AppId").Value,
                Configuration.GetSection("LUIS:AppKey").Value,
                new Uri(Configuration.GetSection("LUIS:Domain").Value));
            var luisRecognizer = new LuisRecognizer(luisModel);
            return luisRecognizer;
        }

        private QnAMaker CreateQnAMaker()
        {
            var qnaMakerEndpoint = new QnAMakerEndpoint
            {
                KnowledgeBaseId = Configuration.GetSection("QnAMaker:KnowledgeBaseId").Value,
                Host = Configuration.GetSection("QnAMaker:Endpoint").Value,
                EndpointKey = Configuration.GetSection("QnAMaker:SubscriptionKey").Value
            };
            var qnaMakerOptions = new QnAMakerOptions
            {
                ScoreThreshold = Single.Parse(Configuration.GetSection("QnAMaker:ScoreThreshold").Value, CultureInfo.InvariantCulture),
                Top = Int32.Parse(Configuration.GetSection("QnAMaker:TopResults").Value)
            };

            var qnaMaker = new QnAMaker(qnaMakerEndpoint, qnaMakerOptions);
            return qnaMaker;
        }

        private TextAnalyticsOptions CreateTextAnalyticsOptions()
        {
            return new TextAnalyticsOptions
            {
                AnalyzeSentiment = true,
                ExtractKeyPhrases = true,
                SentimentWordThreshold = 3,
                TextAnalyticsKey = Configuration.GetSection("TextAnalytics:Key").Value,
                TextAnalyticsEndpoint = Configuration.GetSection("TextAnalytics:Endpoint").Value
            };
        }

        private ContentModeratorOptions CreateContentModeratorOptions()
        {
            var contentModeratorOptions = new ContentModeratorOptions
            {
                ServiceKey = Configuration.GetSection("ContentModerator:Key").Value,
                ServiceBaseUrl = Configuration.GetSection("ContentModerator:BaseUrl").Value
            };
            return contentModeratorOptions;
        }

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseBotFramework();
        }
    }
}