using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(HistDocumentMetadata))]
    public partial class HistDocument
    {
        private SASContext db = new SASContext();
        public HistDocument(Document doc)
        {
            documentID = doc.ID;
            documentParentID = doc.documentParentID;
            documentTypeID = doc.documentTypeID;
            companyID = doc.companyID;
            EDT = doc.EDT;
            title = doc.title;
            description = doc.description;
            documentText = doc.documentText;
            url = doc.url;
            createUser = doc.createUser;
            version = doc.version;
            responsible = doc.responsible.Value;
        }

        public HistDocument()
        {
        }

        [NotMapped]
        public User CreateUser
        {
            get
            {
                return db.Users.Find(createUser);
            }
        }
    }

    public class HistDocumentMetadata
    {
        [Key]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> dateAdded { get; set; }
    }
}
