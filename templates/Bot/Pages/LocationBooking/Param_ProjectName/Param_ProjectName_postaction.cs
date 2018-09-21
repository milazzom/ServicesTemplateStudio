using Microsoft.Bot.Schema;
//{[{
using Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs;
using Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs.AdaptiveCards;
using Microsoft.Services.BotTemplates.Param_ProjectName.Services;
//}]}

namespace Microsoft.Services.BotTemplates.Param_ProjectName
{
    /// <summary>
    /// This is a very simple demo bot based on LUIS
    /// </summary>
    public class Param_ProjectName : IBot
    {
        private void ConfigureDialogs()
        {
//{[{
            BingMaps bingMaps = _services.GetRequiredService(typeof (BingMaps)) as BingMaps; //Singleton
            _dialogs.Add("BookRestaurant", new BookRestaurant(_luisRecognizer, bingMaps));
//}]}
        }
    }
}