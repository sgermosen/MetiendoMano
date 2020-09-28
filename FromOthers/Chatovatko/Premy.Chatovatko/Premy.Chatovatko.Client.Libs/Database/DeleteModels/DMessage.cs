using Premy.Chatovatko.Client.Libs.Database.Models;

namespace Premy.Chatovatko.Client.Libs.Database.DeleteModels
{
    public class DMessage : IDeleteModel
    {
        private readonly Messages message;
        private readonly long myUserId;

        public DMessage(Messages message, long myUserId)
        {
            this.message = message;
            this.myUserId = myUserId;
        }

        public void DoDelete(Context context)
        {
            context.Messages.Remove(message);
            PushOperations.DeleteBlobMessage(context, message.GetBlobId(), myUserId);
        }
    }
}
