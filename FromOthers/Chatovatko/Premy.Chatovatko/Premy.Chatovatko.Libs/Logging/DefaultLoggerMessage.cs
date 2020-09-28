using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.Logging
{
    public class DefaultLoggerMessage : ILoggerMessage
    {
        private readonly String className;
        private String message;
        private readonly String source;
        private readonly DateTime timeOfCreation;
        private readonly bool error;

        public DefaultLoggerMessage(String className, String message, String source, DateTime timeOfCreation, bool error)
        {
            this.className = className;
            this.message = message;
            this.source = source;
            this.timeOfCreation = timeOfCreation;
            this.error = error;
        }

        public bool IsError => error;

        public string GetClassName() => className;

        public string GetMessage() => message;
        public string SetMessage(string message) => this.message = message;

        public string GetSource() => source;

        public DateTime GetTimeOfCreation() => timeOfCreation;
    }
}
