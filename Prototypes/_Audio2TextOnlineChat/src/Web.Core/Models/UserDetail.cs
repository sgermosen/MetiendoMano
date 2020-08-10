using System;

namespace Web.Core.Models
{
    public class UserDetail
    {
        public Guid UserId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public bool IsOnHold { get; set; }
        public Guid ConnectedWith { get; set; }
    }
}
