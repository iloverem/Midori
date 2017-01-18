using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidoriBot
{
    public class MidoriConfig
    {
        public static bool AcceptBotCommands = false;
        public static ulong OwnerId = 255950165200994307;
        public static bool AlertOnUnknownCommand = true;
        public static string BotDescription = "Hi! I'm Midori, a bot made by Lofdat for Discord.";
        public static string CommandPrefix = Environment.GetEnvironmentVariable("Midori_CommandPrefix");
        public static string ConnectionToken = Environment.GetEnvironmentVariable("Midori_ConnectionToken");
        public static string MidoriVersion = "1.0";
    }
}
