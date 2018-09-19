using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Recognizers.Text;
using Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs.Prompts;
using Microsoft.Services.BotTemplates.Param_ProjectName.Schema;
using Microsoft.Services.BotTemplates.Param_ProjectName.Services;
using ChoicePrompt = Microsoft.Bot.Builder.Dialogs.ChoicePrompt;

namespace Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs
{
    public class BookRestaurant : DialogContainer
    {
        private readonly BingMaps _bingMaps;

        public BookRestaurant(LuisRecognizer luisRecognizer, BingMaps bingMaps) : base("BookRestaurant")
        {
            _bingMaps = bingMaps;
            Dialogs.Add("AskRestaurantPromt", new ChoicePrompt(Culture.English, ValidateRestaurant) {Style = ListStyle.List});
            //_dialogs.Add("ConfirmRestaurantPrompt", new ConfirmPrompt(Culture.English, ValidateConfirmRestaurant));
            Dialogs.Add("ConfirmRestaurantPrompt", new LuisConfirmPrompt(Culture.English, luisRecognizer, ValidateConfirmRestaurant));
            Dialogs.Add(DialogId, new WaterfallStep[]
            {
                AskForRestaurant,
                ConfirmRestaurantSelection,
                FinalizeBooking
            });
        }

        private async Task AskForRestaurant(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            var geoLocation = ChannelData.GetChannelDataFromActivity(dc.Context.Activity).AdditionalJTokens["geolocation"];
            var state = _bingMaps.GetState(double.Parse(geoLocation["latitude"].ToString()), double.Parse(geoLocation["longitude"].ToString()));
            var restaurants = FakeRestaurants.GetRestaurants(state).Select(r => r.Name).ToList();

            //var restaurants = FakeRestaurants.GetRestaurants().Select(r => r.Name).ToList();
            var po = new ChoicePromptOptions
            {
                Choices = ChoiceFactory.ToChoices(restaurants),
                RetryPromptString = "Sorry, didn't get that"
            };

            await dc.Prompt("AskRestaurantPromt", "Sure thing, I've found the following restaurants:", po);
        }

        private async Task ValidateRestaurant(ITurnContext context, ChoiceResult result)
        {
            await context.SendActivity($"{result.Value.Value} sounds great!");
        }

        private async Task ConfirmRestaurantSelection(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            var textResult = (ChoiceResult)args;
            var po = new PromptOptions
            {
                RetryPromptString = "Sorry, didn't get that"
            };
            await dc.Prompt("ConfirmRestaurantPrompt", $"Make a reservation at {textResult.Value.Value}, right?", po);
        }

        private async Task ValidateConfirmRestaurant(ITurnContext context, ConfirmResult result)
        {
            if (result.Confirmation)
            {
                await context.SendActivity("Got it!");
            }
            else
            {
                await context.SendActivity("Let's do this again");
            }
        }

        private async Task FinalizeBooking(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            var result = (ConfirmResult)args;
            if (result.Confirmation)
            {
                await dc.Context.SendActivity("Let's get you a reservation.");
            }
            else
            {
                // Start over
                await dc.Replace("BookRestaurant", args);
            }
        }
    }
}