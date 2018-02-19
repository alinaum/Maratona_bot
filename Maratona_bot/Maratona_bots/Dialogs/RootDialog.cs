using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Maratona_bots.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var message = activity.CreateReply();

            // return our reply to the user
           // await context.PostAsync("**Ola, tudo bem**");

            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {

                var heroCard = new HeroCard();
                heroCard.Title = "Planeta";
                heroCard.Subtitle = "Univeso";
                heroCard.Images = new List<CardImage>
            {
                new CardImage("https://ed.i.uol.com.br/album/planetas_curiosidades_f_001.jpg", "Planeta")
            };

                message.Attachments.Add(heroCard.ToAttachment());
            }
            else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var videocard = new VideoCard();
                videocard.Title = "Video Teste";
                videocard.Subtitle = "Sub video Teste";
                videocard.Autostart = true;
                videocard.Autoloop = false;
                videocard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4")


                };
                message.Attachments.Add(videocard.ToAttachment());
            }
            else if(activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAudio();
                message.Attachments.Add(attachment);

            }
            else if (activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAnimation();
                message.Attachments.Add(attachment);
            }

            else if (activity.Text.Equals("carousel", StringComparison.InvariantCultureIgnoreCase))
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                var audio = CreateAudio();
                var animation = CreateAnimation();

                message.Attachments.Add(audio);
                message.Attachments.Add(animation);
            }

                await context.PostAsync(message);


            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAnimation()
        {
            var animationcard = new AnimationCard();
            animationcard.Title = "Teste audio";
            animationcard.Subtitle = "Sub audio Teste";
            animationcard.Autostart = true;
            animationcard.Autoloop = false;
            animationcard.Media = new List<MediaUrl>
                {
                    new MediaUrl("https://media.giphy.com/media/AGOPaltgJ2pBC/giphy.gif")
                };
            return animationcard.ToAttachment();
        }

        private Attachment CreateAudio()
        {
            var audiocard = new AudioCard();
            audiocard.Title = "Teste audio";
            audiocard.Subtitle = "Sub audio Teste";
            audiocard.Autostart = true;
            audiocard.Autoloop = false;
            audiocard.Image = new ThumbnailUrl("");
            audiocard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://www.wavlist.com/soundfx/024/wind-thunder.wav")
                };

            return audiocard.ToAttachment();
        }
    }

}
