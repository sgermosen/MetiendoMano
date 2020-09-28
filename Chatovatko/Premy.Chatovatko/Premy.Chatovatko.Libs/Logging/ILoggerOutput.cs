using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Premy.Chatovatko.Libs.Logging
{
    public interface ILoggerOutput
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        void Log(ILoggerMessage message);
        String GetName();
        void Close();
    }
}
