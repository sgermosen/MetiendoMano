using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.DeleteModels
{
    public interface IDeleteModel
    {
        void DoDelete(Context context);
    }
}
