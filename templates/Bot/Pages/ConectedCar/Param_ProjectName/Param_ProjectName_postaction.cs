using Microsoft.Bot.Schema;
//{[{
using Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs;
using Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs.AdaptiveCards;
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
            _dialogs.Add("TrunkStatusChanged", new TrunkStatusChanged());
//}]}
        }
    }
}