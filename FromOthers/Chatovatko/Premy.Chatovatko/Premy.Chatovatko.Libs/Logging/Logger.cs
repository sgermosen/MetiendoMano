using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.Logging
{
    public class Logger
    {
        private List<ILoggerOutput> outputs;

        public Logger()
        {
            outputs = new List<ILoggerOutput>();
        }

        public Logger(ILoggerOutput output) :this()
        {
            outputs.Add(output);
        }

        public List<ILoggerOutput> LoggerOutputs { get => outputs; }

        public void Log(ILoggerMessage message)
        {
            foreach(ILoggerOutput output in outputs)
            {
                output.Log(message);
            }
        }

        public void Close()
        {
            foreach (ILoggerOutput output in outputs)
            {
                output.Close();
            }
        }

        public void Log(String name, String source, String message, bool error)
        {
            DefaultLoggerMessage theLogMessage = new DefaultLoggerMessage(name, message, source, DateTime.Now, error);
            Log(theLogMessage);
        }

        public void LogException(Exception ex, String name, String source, String message)
        {
            DefaultLoggerMessage theLogMessage = new DefaultLoggerMessage(name, message, source, DateTime.Now, true);
            LogException(theLogMessage, ex);
        }

        public void Log(ILoggable me, String message, bool error)
        {
            DefaultLoggerMessage theLogMessage = new DefaultLoggerMessage(me.GetType().Name, message, me.GetLogSource(), DateTime.Now, error);
            Log(theLogMessage);
        }

        public void Log(object me, String message, bool error)
        {
            DefaultLoggerMessage theLogMessage = new DefaultLoggerMessage(me.GetType().Name, message, "", DateTime.Now, error);
            Log(theLogMessage);
        }

        public void LogException(ILoggable me, Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            while (exception != null)
            {
                builder.Append(exception.GetType().Name);
                builder.Append("\n");
                builder.Append(exception.Message);
                builder.Append("\n");
                builder.Append(exception.StackTrace);
                builder.Append("\n\n");
                exception = exception.InnerException;
            }
            Log(me, builder.ToString(), true);
        }

        private void LogException(DefaultLoggerMessage me, Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(me.GetMessage());
            builder.Append("\n");
            while (exception != null)
            {
                builder.Append(exception.GetType().Name);
                builder.Append("\n");
                builder.Append(exception.Message);
                builder.Append("\n");
                builder.Append(exception.StackTrace);
                builder.Append("\n\n");
                exception = exception.InnerException;
            }
            me.SetMessage(builder.ToString());
            Log(me);
        }
        
        public void Log(ILoggable me, String message) => Log(me, message, false);

        public void Log(object me, String message) => Log(me, message, false);
    }
}
