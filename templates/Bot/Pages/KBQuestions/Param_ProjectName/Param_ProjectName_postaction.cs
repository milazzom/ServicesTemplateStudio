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
             QnAMaker qnaMaker = _services.GetRequiredService(typeof (QnAMaker)) as QnAMaker; //Singleton
            _dialogs.Add("KBQuestion", new KBQuestion(qnaMaker));
//}]}
        }
    }
}