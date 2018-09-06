using System;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Common.Telemetry
{
    /// <summary>
    /// Common property names for event properties (logged in AppInsights under customProperties).
    /// </summary>
    public class TelemetryProperties
    {
        /// <summary>
        /// The Channel ID.
        /// </summary>
        public const string ChannelId = "Channel";

        /// <summary>
        /// The FromId property.
        /// </summary>
        public const string FromId = "FromId";

        /// <summary>
        /// The FromName property.
        /// </summary>
        public const string FromName = "FromName";

        /// <summary>
        /// The ConversationId property.
        /// </summary>
        public const string ConversationId = "ConversationId";

        /// <summary>
        /// The ConversationName property.
        /// </summary>
        public const string ConversationName = "ConversationName";

        /// <summary>
        /// The ClientInfo entity contained in the Entities collection of the activity.
        /// </summary>
        /// <remarks>
        /// Not all the channels send clientInfo, so far, only Skype and MSTeams do.
        /// </remarks>
        public const string ClientInfo = "ClientInfo";

        /// <summary>
        /// Activity overflow properties
        /// </summary>
        public const string ActivityProperties = "ActivityProperties";

        /// <summary>
        /// The Text property.
        /// </summary>
        public const string Text = "Text";

        /// <summary>
        /// The Locale property.
        /// </summary>
        public const string Locale = "Locale";

        /// <summary>
        /// The English language name corresponding to the locale (as resolved by CultureInfo).
        /// </summary>
        public const string Language = "Language";

        /// <summary>
        /// The Key Phrases property.
        /// </summary>
        public const string KeyPhrases = "KeyPhrases";

        /// <summary>
        /// The Sentiment Analysis result.
        /// </summary>
        public const string SentimentAnalysisResult = "SentimentAnalysisResult";

        /// <summary>
        /// The Question property.
        /// </summary>
        public const string QuestionProperty = "Question";

        /// <summary>
        /// The Intent property.
        /// </summary>
        public const string IntentProperty = "Intent";

        /// <summary>
        /// The KnowledgeItemsDiscarded property.
        /// </summary>
        public const string KnowledgeItemsDiscardedProperty = "KnowledgeItemsDiscarded";

        /// <summary>
        /// The QNAResponse property.
        /// </summary>
        public const string QnAResponseProperty = "QnAResponse";

        /// <summary>
        /// The Error property.
        /// </summary>
        public const string ErrorProperty = "Error";

        /// <summary>
        /// The ErrorHeadline property.
        /// </summary>
        public const string ErrorHeadlineProperty = "ErrorHeadline";

        /// <summary>
        /// The ErrorData property.
        /// </summary>
        public const string ErrorDataProperty = "ErrorData";

        /// <summary>
        /// The key for storing the LUIS model
        /// </summary>
        public const string LuisAppId = "LuisAppId";

        /// <summary>
        /// The Id of the Bot logging the event.
        /// </summary>
        /// <remarks>
        /// If your solution has multiple subbots, this will allow you to segment telemetry for each bot.
        /// </remarks>
        public const string BotId = "BotId";

        /// <summary>
        /// The ID of the QnAMaker KB being used.
        /// </summary>
        public const string QnAMakerAppId = "QnAMakerAppId";

        /// <summary>
        /// The first question that matches the QnAResponse found as it shows in the KB.
        /// This gives us a normalized view of the questions and it is 
        /// useful to build a report and find the "true" FAQs
        /// </summary>
        public const string QnAMakerTopQuestion = "QnAMakerTopQuestion";

        /// <summary>
        /// The first answer for the QnA Maker question (with the highest score)
        /// </summary>
        public const string QnAMakerTopAnswer = "QnAMakerTopAnswer";

        /// <summary>
        /// A serialize string array with all the questions that map to the top answer found in QnAMaker
        /// </summary>
        public const string QnAMakerAllQuestions = "QnAMakerAllQuestions";

        /// <summary>
        /// A serialized json with all the answers for the question. 
        /// </summary>
        public const string QnAMakerAllAnswers = "QnAMakerAllAnswers";

        /// <summary>
        /// The question the user asked.
        /// </summary>
        public const string QnAMakerQuestion = "QnAMakerQuestion";

        /// <summary>
        /// A Json object representing the whole TopIntent (includes intent and entities if any)
        /// </summary>
        public const string IntentInfo = "IntentInfo";

        /// <summary>
        /// The name of the flow being executed.
        /// </summary>
        public const string FlowName = "FlowName";

        /// <summary>
        /// The name of the step that is being exectued in the flow.
        /// </summary>
        public const string FlowStep = "FlowStep";

        public const string FlowStatus = "FlowStatus";

        public const string FlowInstanceId = "FlowInstanceId";

        public const string NumberOfUserPrompts = "NumberOfUserPrompts";

        public const string FlowStartedStatus = "Started";

        public const string FlowCanceledStatus = "Canceled";

        public const string FlowAgentHandoffStatus = "AgentHandoff";

        public const string FlowEndStatus = "Done";

        /// <summary>
        /// (Outbound to bot only) Id of the bot that received the message
        /// </summary>
        public const string RecipientId = "RecipientId";

        /// <summary>
        /// (Outbound to bot only) Name of the bot that received the message
        /// </summary>
        public const string RecipientName = "RecipientName";

        public const string ChannelData = "ChannelData";

        /// <summary>
        /// Open ended value sent with the activity (normally happens when the user clicks on a button on an adaptive card).
        /// </summary>
        public const string Value = "Value";

        /// <summary>
        /// The ActivityId to which the Bot Reply is associated to BotMessageSent.ReplyToId = BotMessageReceived.ActivityId
        /// </summary>
        public const string ReplyToId = "ReplyToId";

        /// <summary>
        /// string reprents the ActivityId BotMessageSent.ReplyToId = BotMessageReceived.ActivityId
        /// </summary>
        public const string ActivityId = "ActivityId";

        /// <summary>
        /// The type of the activity (message, conversationUpdate, typing, etc.)
        /// </summary>
        public const string ActivityType = "ActivityType";

        /// <summary>
        /// The activity serialized as json.
        /// </summary>
        /// <remarks>
        /// Note: this property may contain sensitive data, consider disabling it for production use and only log
        /// the activity properties that you need for reporting.
        /// </remarks>
        public const string ActivityInfo = "ActivityInfo";

        /// <summary>
        /// The Speak property of the Bot response. Sometimes Speak has more details than text.
        /// </summary>
        public const string Speak = "Speak";

        /// <summary>
        /// Name of the operation to invoke or the name of the
        /// event
        /// </summary>
        public static string Name = "Name";
    }
}