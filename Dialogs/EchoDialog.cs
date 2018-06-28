using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;

using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.Bot.Connector.Teams.Models;
using System.Diagnostics;

namespace Microsoft.Bot.Sample.O365CardsBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        private static Attachment CreateSampleO365ConnectorCard()
        {
            #region Multichoice Card
            var multichoiceCard = new O365ConnectorCardActionCard(
            O365ConnectorCardActionCard.Type,
            "Multiple Choice",
            "Multiple Choice Card",
            new List<O365ConnectorCardInputBase>
            {
                new O365ConnectorCardMultichoiceInput(O365ConnectorCardMultichoiceInput.Type,
                "CardsType",
                true,
                "Pick multiple options",
                null,
                new List<O365ConnectorCardMultichoiceInputChoice>{
                    new O365ConnectorCardMultichoiceInputChoice("Hero Card", "Hero Card"),
                    new O365ConnectorCardMultichoiceInputChoice("Thumbnail Card", "Thumbnail Card"),
                    new O365ConnectorCardMultichoiceInputChoice("O365 Connector Card", "O365 Connector Card")
                },
                "expanded",
                true),
                new O365ConnectorCardMultichoiceInput(O365ConnectorCardMultichoiceInput.Type,
                "Teams",
                true,
                "Pick multiple options",
                null,
                new List<O365ConnectorCardMultichoiceInputChoice>
                {
                    new O365ConnectorCardMultichoiceInputChoice("Bot", "Bot"),
                    new O365ConnectorCardMultichoiceInputChoice("Tab", "Tab"),
                    new O365ConnectorCardMultichoiceInputChoice("Connector", "Connector"),
                    new O365ConnectorCardMultichoiceInputChoice("Compose Extension", "Compose Extension")
                },
                "compact",
                true),
                new O365ConnectorCardMultichoiceInput(O365ConnectorCardMultichoiceInput.Type,"Apps",false,"Pick an App",null,
                new List<O365ConnectorCardMultichoiceInputChoice>
                {
                    new O365ConnectorCardMultichoiceInputChoice("VSTS", "VSTS"),
                    new O365ConnectorCardMultichoiceInputChoice("Wiki", "Wiki"),
                    new O365ConnectorCardMultichoiceInputChoice("Github", "Github")
                },
                "expanded",
                false),
                new O365ConnectorCardMultichoiceInput(O365ConnectorCardMultichoiceInput.Type,"OfficeProduct",  
                false,
                "Pick an Office Product",
                null,
                new List<O365ConnectorCardMultichoiceInputChoice>
                {
                    new O365ConnectorCardMultichoiceInputChoice("Outlook", "Outlook"),
                    new O365ConnectorCardMultichoiceInputChoice("MS Teams", "MS Teams"),
                    new O365ConnectorCardMultichoiceInputChoice("Skype", "Skype")
                },
                "compact",
                false)
            },
            new List<O365ConnectorCardActionBase>
            {
                new O365ConnectorCardHttpPOST(O365ConnectorCardHttpPOST.Type,
                "Send",
                "multichoice",
                @"{""CardsType"":""{{CardsType.value}}"", ""Teams"":""{{Teams.value}}"", ""Apps"":""{{Apps.value}}"", ""OfficeProduct"":""{{OfficeProduct.value}}""}")
            });

            #endregion

            #region Input Card
            var inputCard = new O365ConnectorCardActionCard(
                O365ConnectorCardActionCard.Type,
                "Text Input",
                "Input Card",
                new List<O365ConnectorCardInputBase>
                {
                new O365ConnectorCardTextInput(
                    O365ConnectorCardTextInput.Type,
                    "text-1",
                    false,
                    "multiline, no maxLength",
                    null,
                    true,
                    null),
                new O365ConnectorCardTextInput(
                    O365ConnectorCardTextInput.Type,
                    "text-2",
                    false,
                    "single line, no maxLength",
                    null,
                    false,
                    null),
                new O365ConnectorCardTextInput(
                    O365ConnectorCardTextInput.Type,
                    "text-3",
                    true,
                    "multiline, max len = 10, isRequired",
                    null,
                    true,
                    10),
                new O365ConnectorCardTextInput(
                    O365ConnectorCardTextInput.Type,
                    "text-4",
                    true,
                    "single line, max len = 10, isRequired",
                    null,
                    false,
                    10)
                },
                new List<O365ConnectorCardActionBase>
                {
                new O365ConnectorCardHttpPOST(
                    O365ConnectorCardHttpPOST.Type,
                    "Send",
                    "inputText",
                    @"{""text1"":""{{text-1.value}}"", ""text2"":""{{text-2.value}}"", ""text3"":""{{text-3.value}}"", ""text4"":""{{text-4.value}}""}")
                });
            #endregion

            #region Date Card
            var dateCard = new O365ConnectorCardActionCard(
                O365ConnectorCardActionCard.Type,
                "Date Input",
                "Date Card",
                new List<O365ConnectorCardInputBase>
                {
                new O365ConnectorCardDateInput(
                    O365ConnectorCardDateInput.Type,
                    "date-1",
                    true,
                    "date with time",
                    null,
                    true),
                new O365ConnectorCardDateInput(
                    O365ConnectorCardDateInput.Type,
                    "date-2",
                    false,
                    "date only",
                    null,
                    false)
                },
                new List<O365ConnectorCardActionBase>
                {
                new O365ConnectorCardHttpPOST(
                    O365ConnectorCardHttpPOST.Type,
                    "Send",
                    "dateInput",
                    @"{""date1"":""{{date-1.value}}"", ""date2"":""{{date-2.value}}""}")
                });
            #endregion

            var section = new O365ConnectorCardSection(
                "**section title**",
                "section text",
                "activity title",
                "activity subtitle",
                "activity text",
                "http://connectorsdemo.azurewebsites.net/images/MSC12_Oscar_002.jpg",
                "Avatar",
                true,
                new List<O365ConnectorCardFact>
                {
                    new O365ConnectorCardFact("Fact name 1", "Fact value 1"),
                    new O365ConnectorCardFact("Fact name 2", "Fact value 2"),
                },
                new List<O365ConnectorCardImage>
                {
                    new O365ConnectorCardImage
                    {
                        Image = "http://connectorsdemo.azurewebsites.net/images/MicrosoftSurface_024_Cafe_OH-06315_VS_R1c.jpg",
                        Title = "image 1"
                    },
                    new O365ConnectorCardImage
                    {
                        Image = "http://connectorsdemo.azurewebsites.net/images/WIN12_Scene_01.jpg",
                        Title = "image 2"
                    },
                    new O365ConnectorCardImage
                    {
                        Image = "http://connectorsdemo.azurewebsites.net/images/WIN12_Anthony_02.jpg",
                        Title = "image 3"
                    }
                });

            O365ConnectorCard card = new O365ConnectorCard()
            {
                Summary = "O365 card summary",
                ThemeColor = "#E67A9E",
                Title = "card title edit test here",
                Text = "card text",
                Sections = new List<O365ConnectorCardSection> { section },
                PotentialAction = new List<O365ConnectorCardActionBase>
             {
                multichoiceCard,
                inputCard,
                dateCard,
                new O365ConnectorCardViewAction(
                    O365ConnectorCardViewAction.Type,
                    "View Action",
                    null,
                    new List<string>
                    {
                        "http://microsoft.com"
                    })

              }
            };

            return card.ToAttachment();
        }

        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            // console.log WTF
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            var numberOfAttachments = message.Attachments.Count;

            if (numberOfAttachments > 0)
            {
                await context.PostAsync($"{numberOfAttachments} attachment(s) found.");

                for (var i = 0; i < numberOfAttachments; i++)
                {
                    await context.PostAsync($"Attachment #{i + 1} URL: {message.Attachments[i].ContentUrl}");
                    await context.PostAsync($"Attachment #{i + 1} Type: {message.Attachments[i].ContentType}");
                    await context.PostAsync($"Attachment #{i + 1} Name: {message.Attachments[i].Name}");
                }
            }

            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Are you sure you want to reset the count?",
                    "Didn't get that!",
                    promptStyle: PromptStyle.Auto);
            }
            //else if (message.Attachments.Count > 0)
            //{

            //}
            else
            {
                
                try
                {
                    await context.PostAsync($"{this.count++}: You said {message.Text}");

                    var sendThis = CreateSampleO365ConnectorCard();
                    var sendMessage = context.MakeMessage(); // Activity.CreateMessageActivity();
                    sendMessage.Attachments.Add(sendThis);
                    await context.PostAsync(sendMessage);
                }
                catch (Exception ex)
                {
                    await context.PostAsync($"Aw crap: {ex.Message}");
                    Trace.TraceError(ex.Message);
                    Trace.TraceError(ex.StackTrace);
                }

                context.Wait(MessageReceivedAsync);
            }
        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
                var noReset = new HeroCard("Test Hero Card", "Gotta figure out invoke", "Testing with debugger for invoke timing.", null, new[]
                {
                    new CardAction(ActionTypes.ImBack, "ImBack", value: "Wikipedia"),
                    new CardAction(ActionTypes.MessageBack, "MessageBack", value: "Wookiee"),
                    new CardAction("invoke", "MessageBack", value: "Null")
                });
                var sendThis = noReset.ToAttachment();
                var sendMessage = context.MakeMessage();
                sendMessage.Attachments.Add(sendThis);
                await context.PostAsync(sendMessage);
            }
            context.Wait(MessageReceivedAsync);
        }

    }
}