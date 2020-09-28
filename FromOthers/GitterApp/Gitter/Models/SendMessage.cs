using Newtonsoft.Json;

namespace Gitter.Models
{
    public class SendMessage
    {
        public SendMessage(string text)
        {
            this.Text = text;
        }

        [JsonProperty("text")]
        public string Text { get; private set; }
    }
}