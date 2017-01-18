using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MidoriBot.Common
{
    public enum LogLevel
    {
        Info,
        Warning,
        Danger,
        Crit
    }

    public enum LogEvent
    {
        Command,
        Breakpoint
    }

    public static class Logging
    {
        public static void LogMessage(LogLevel Level, LogEvent Event, string LogMessage)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine("=====");
            switch (Level)
            {
                case LogLevel.Info:
                    Builder.AppendLine("Log level: Information");
                    break;
                case LogLevel.Warning:
                    Builder.AppendLine("Log level: Warning");
                    break;
                case LogLevel.Danger:
                    Builder.AppendLine("Log level: Danger");
                    break;
                case LogLevel.Crit:
                    Builder.AppendLine("Log level: CRITICAL");
                    break;
            }
            switch (Event)
            {
                case LogEvent.Breakpoint:
                    Builder.AppendLine("Reached breakpoint.");
                    break;
                case LogEvent.Command:
                    Builder.AppendLine("Log type: Command");
                    break;
            }
        }
    }
}
