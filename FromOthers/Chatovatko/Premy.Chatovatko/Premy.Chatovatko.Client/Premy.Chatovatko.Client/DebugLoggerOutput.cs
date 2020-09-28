using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Premy.Chatovatko.Client
{
    public class DebugLoggerOutput : ILoggerOutput
    {
        public void Close()
        {

        }

        public string GetName()
        {
            return this.GetType().Name;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Log(ILoggerMessage message)
        {
            String theText = String.Format("{0}; {1}; {2}: {3}\n", message.GetTimeOfCreation().ToLongTimeString(), message.GetSource(), message.GetClassName(), message.GetMessage());
            System.Diagnostics.Debug.Write(theText);
            
        }
    }
}
