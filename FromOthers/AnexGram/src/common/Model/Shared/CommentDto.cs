using System;

namespace Model.Shared
{
    public class CommentListFilter
    {
        public int? PhotoId { get; set; }
    }

    public class CommentListDto
    {
        public long CommentId { get; set; }
        public string Comment { get; set; }
        public int PhotoId { get; set; }
        public string User { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CommentCreateDto
    {
        public string Comment { get; set; }
        public int PhotoId { get; set; }
        public string UserId { get; set; }
    }
}
