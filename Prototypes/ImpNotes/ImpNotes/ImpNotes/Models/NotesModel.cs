using System;
using SQLite;

namespace ImpNotes.Models
{
    public class NotesModel
    {
        [AutoIncrement, PrimaryKey]
       // public string Id { get; set; }
       public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string TextColor { get; set; }

        public NotesModel()
        {
            CreatedDate = DateTime.UtcNow;
           // Id = Guid.NewGuid().ToString();
        }

        public override string ToString() => $"{Title} {CreatedDate}";

    }
}
