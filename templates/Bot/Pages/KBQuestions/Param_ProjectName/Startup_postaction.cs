using Microsoft.Extensions.DependencyInjection;
//{[{
using Microsoft.Services.BotTemplates.Param_ProjectName.Common.AI.ContentModerator;
using Microsoft.Services.BotTemplates.Param_ProjectName.Common.AI.TextAnalytics;
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
            // Register QnAMaker service
            var qnaMaker = CreateQnAMaker();
            services.AddSingleton(qnaMaker);
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
                IStorage dataStore = new MemoryStorage();

                options.Middleware.Add(new ConversationState<Dictionary<string, object>>(dataStore));
                options.Middleware.Add(new TextAnalyticsMiddleware(CreateTextAnalyticsOptions()));
                options.Middleware.Add(new ContentModeratorMiddleware(CreateContentModeratorOptions()));
 //}]}
            });
        }
//{[{

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
//}]}
    }
}