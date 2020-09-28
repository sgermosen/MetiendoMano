using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Sync
{
    public interface IAction
    {
        bool Run();
        IAction GetNext();
    }
}
