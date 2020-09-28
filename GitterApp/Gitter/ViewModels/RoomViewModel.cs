using System;
using Gitter.Models;

namespace Gitter.ViewModels
{
    public class RoomViewModel
    {
        public RoomViewModel(Room room)
        {
            if (room == null)
                throw new ArgumentNullException("room");

            this.Room = room;
        }

        public Uri UserAvatarSource
        {
            get
            {
                if (this.Room.user != null && this.Room.user.avatarUrlSmall != null)
                {
                    return new Uri(this.Room.user.avatarUrlSmall);
                }

                return null;
            }
        }

        public string Id
        {
            get { return this.Room.id; }
        }

        public string Name
        {
            get { return this.Room.name; }
        }

        public string Topic
        {
            get { return this.Room.topic; }
        }

        public Room Room { get; private set; }
    }
}