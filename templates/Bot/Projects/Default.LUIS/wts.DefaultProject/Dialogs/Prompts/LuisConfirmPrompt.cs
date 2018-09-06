using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using ConfirmPrompt = Microsoft.Bot.Builder.Dialogs.ConfirmPrompt;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Dialogs.Prompts
{
    public class LuisConfirmPrompt : ConfirmPrompt
    {
        private readonly LuisRecognizer _luisRecognizer;

        public LuisConfirmPrompt(string culture, LuisRecognizer luisRecognizer, PromptValidatorEx.PromptValidator<ConfirmResult> validator = null)
            : base(culture, validator)
        {
            _luisRecognizer = luisRecognizer;
        }

        protected override async Task<ConfirmResult> OnRecognize(DialogContext dc, PromptOptions options)
        {
            var result = await base.OnRecognize(dc, options);
            if (!result.Confirmation)
            {
                var luisResult = await _luisRecognizer.Recognize(dc.Context.Activity.Text, CancellationToken.None);
                var topIntent = luisResult.GetTopScoringIntent().intent;
                if (topIntent == "ConfirmYes" || topIntent == "ConfirmNo")
                {
                    result.Confirmation = luisResult.GetTopScoringIntent().intent == "ConfirmYes";
                    result.Status = PromptStatus.Recognized;
                }
                else
                {
                    result.Status = PromptStatus.NotRecognized;
                }
            }

            return result;
        }
    }
}