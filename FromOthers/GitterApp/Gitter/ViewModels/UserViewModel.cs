using System;
using Gitter.Models;

namespace Gitter.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            this.User = user;
        }

        public Uri AvatarSource
        {
            get
            {
                if (this.User.avatarUrlSmall != null)
                {
                    return new Uri(this.User.avatarUrlSmall);
                }

                return null;
            }
        }

        public string DisplayName
        {
            get { return this.User.displayName; }
        }

        public string UserName
        {
            get { return this.User.username; }
        }

        public User User { get; private set; }
    }
}

