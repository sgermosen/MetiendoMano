using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.UpdateModels
{
    public interface IUpdateModel
    {
        UpdateModelTypes GetModelType();
        JsonCapsula GetSelfUpdate();
        JsonCapsula GetRecepientUpdate();

    }
}
