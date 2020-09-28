using System;
using Gitter.Models;
using Xamarin.Forms;
using Humanizer;

namespace Gitter.ViewModels
{
    public class MessageViewModel
    {
        private readonly Message message;

        public MessageViewModel(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            this.message = message;
        }

        public string Sender
        {
            get
            {
                if (this.message.fromUser != null)
                {
                    return this.message.fromUser.username;
                }

                return null;
            }
        }

        public Uri SenderAvatarSource
        {
            get
            {
                if (this.message.fromUser != null && this.message.fromUser.avatarUrlSmall != null)
                {
                    return new Uri(this.message.fromUser.avatarUrlSmall);
                }

                return null;
            }
        }

        public string Text
        {
            get { return this.message.text; }
        }

        public string Sent
        {
            get
            {
                if (!string.IsNullOrEmpty(this.message.sent))
                {
                    string sent = this.message.sent;

                    // The Gitter streaming API sends a UTC time, but doesn't specify it explicilty (the normal message API does), 
                    // we have to do that for ourselves
                    if (!sent.EndsWith("Z"))
                    {
                        sent = sent + "Z";
                    }

                    DateTime parsed;
                    if (DateTime.TryParse(sent, out parsed))
                    {
                        return parsed.Humanize(false);
                    }
                }
                return string.Empty;
            }
        }
    }
}