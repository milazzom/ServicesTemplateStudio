using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Ai.QnA;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Services.BotTemplates.Param_ProjectName.Common.AI.ContentModerator;
using Microsoft.Services.BotTemplates.Param_ProjectName.Common.AI.TextAnalytics;
using Microsoft.Services.BotTemplates.Param_ProjectName.Common.Extensions;

namespace Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs
{
    public class KBQuestion : IDialogContinue
    {
        private readonly QnAMaker _qnaMaker;

        public KBQuestion(QnAMaker qnAMaker)
        {
            _qnaMaker = qnAMaker;
        }

        public async Task DialogContinue(DialogContext dc)
        {
            await SearchKBAndShowResults(dc);
            await dc.End();
        }

        public async Task DialogBegin(DialogContext dc, IDictionary<string, object> dialogArgs = null)
        {
            await SearchKBAndShowResults(dc);
            await dc.End();
        }

        private async Task SearchKBAndShowResults(DialogContext dc)
        {
            var kbFound = await _qnaMaker.GetAnswers(dc.Context.Activity.Text);
            Activity reply;
            if (kbFound.Length > 0)
            {
                reply = dc.Context.Activity.CreateReply(kbFound.First().Answer);
            }
            else
            {
                reply = dc.Context.Activity.CreateReply("Sorry, didn't get that");
            }

            if (TextAnalyticsMiddleware.IsLowSentiment(dc.Context, .4) || ContentModeratorMiddleware.CurseWordsDetected(dc.Context))
            {
                reply.Text = reply.Text + " \r\n  Would you like to talk to an agent?";
                reply.AddSuggestedActions(new List<string> {"Talk to an agent"});
            }

            reply.Speak = reply.Text;
            await dc.Context.SendActivity(reply);
        }
    }
}