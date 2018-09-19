using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using AdaptiveCards;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace Microsoft.Services.BotTemplates.Param_ProjectName.Dialogs.AdaptiveCards
{
    public class AdaptiveCardHelper
    {
        private static readonly Regex _cardTokensRegex = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);

        /// <summary>
        /// Creates and returns an attachment that gets created from a json file with the card definition.
        /// </summary>
        /// <param name="jsonFile">The name of the json file to use (should be in the Assets\Cards)</param>
        /// <param name="tokens">A dicitionary containing key/value pairs with tokens that will be replaced on the card.</param>
        public static Attachment GetCardAttachmentFromJson(string jsonFile, StringDictionary tokens = null)
        {
            var card = GetCardFromJson(jsonFile, tokens);
            return CreateCardAttachment(card);
        }

        public static Attachment CreateCardAttachment(AdaptiveCard card)
        {
            // workaround, added this serialize/deserialize due to an exception thrown when
            // using dialogContext.Prompt with acivities that have adaptive cards attached.
            object oCard = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(card));
            var attachment = new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = oCard
            };
            return attachment;
        }

        /// <summary>
        /// Creates and returns an AdaptiveCard that gets created from a json file with the card definition.
        /// </summary>
        /// <param name="jsonFile">The relative path to the the json file to use (example: Resources\Cards\myCard.json)</param>
        /// <param name="tokens">A dicitionary containing key/value pairs with tokens that will be replaced on the card.</param>
        public static AdaptiveCard GetCardFromJson(string jsonFile, StringDictionary tokens = null)
        {
            var jsonCard = GetJson(jsonFile);
            if (tokens != null)
            {
                // Escape double quotes to avoid breaking the json.
                var escapedTokens = new StringDictionary();
                foreach (string key in tokens.Keys)
                {
                    escapedTokens.Add(key, tokens[key]?.Replace("\"", "\\\""));
                }

                jsonCard = _cardTokensRegex.Replace(jsonCard, match => escapedTokens[match.Groups[1].Value]);
            }

            var card = JsonConvert.DeserializeObject<AdaptiveCard>(jsonCard);
            return card;
        }

        private static string GetJson(string jsonFile)
        {
            var dir = Path.GetDirectoryName(typeof(AdaptiveCardHelper).Assembly.Location);            
            var filePath = Path.Combine(dir, $"{jsonFile}");
            return File.ReadAllText(filePath);
        }
    }
}