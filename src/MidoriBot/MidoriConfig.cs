using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MidoriBot.Common;

namespace MidoriBot
{
    public class MidoriConfig
    {
        public static bool AcceptBotCommands = Extensions.GetBooleanFromString(Environment.GetEnvironmentVariable("Midori_AcceptBotCommands"));
        public static bool AlertOnUnknownCommand = Extensions.GetBooleanFromString(Environment.GetEnvironmentVariable("Midori_AlertOnUnknownCommand"));
        public static string BotDescription = Environment.GetEnvironmentVariable("Midori_BotDescription");
        public static string CommandPrefix = Environment.GetEnvironmentVariable("Midori_CommandPrefix");
        public static string ConnectionToken = Environment.GetEnvironmentVariable("Midori_ConnectionToken");
        public static string MidoriVersion = "1.0";
    }
}
