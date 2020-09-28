using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public interface IJType
    {
        JsonTypes GetJsonType();
        long GetPriority();
    }
}
