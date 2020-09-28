using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.InsertModels
{
    public interface ICInsertModel
    {
        InsertModelTypes GetModelType();
        JsonCapsula GetSelfUpdate();
        JsonCapsula GetRecepientUpdate();

    }
}
