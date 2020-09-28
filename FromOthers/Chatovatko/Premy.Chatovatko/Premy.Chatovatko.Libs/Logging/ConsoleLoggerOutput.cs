using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Premy.Chatovatko.Server.Logging
{
    public class ConsoleLoggerOutput : ILoggerOutput
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
            if (message.IsError)
            {
                Console.Error.Write(theText);
            }
            else
            {
                Console.Out.Write(theText);
            }
        }
        
    }
}
