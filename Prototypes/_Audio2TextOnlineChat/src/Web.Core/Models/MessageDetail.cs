using System;

namespace Web.Core.Models
{
    public class MessageDetail
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string FromUserName { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
