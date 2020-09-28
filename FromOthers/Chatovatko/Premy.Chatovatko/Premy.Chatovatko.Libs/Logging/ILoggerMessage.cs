using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.Logging
{
    public interface ILoggerMessage
    {
        String GetSource();
        String GetClassName();
        String GetMessage();
        DateTime GetTimeOfCreation();
        bool IsError { get; }
    }
}
