using System;
using System.Collections.Generic;
using System.Text;
using Premy.Chatovatko.Client.Libs.Database.JsonModels;

namespace Premy.Chatovatko.Client.Libs.Database.InsertModels
{
    public class CContact: JContact, ICInsertModel
    {
        public InsertModelTypes GetModelType()
        {
            return InsertModelTypes.CONTACT;
        }

        public JsonCapsula GetRecepientUpdate()
        {
            return null;
        }

        public JsonCapsula GetSelfUpdate()
        {
            return new JsonCapsula(this);
        }
    }
}
