using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidoriBot.Events
{
    public static class midori_ReadyEvent
    {
        public static async Task ReadyEvent()
        {
            Console.WriteLine("=====");
            Console.WriteLine((Midori.MidoriClient.GetApplicationInfoAsync().GetAwaiter().GetResult()).Description);
            Console.WriteLine("Active token: " + Midori.MidoriConfig["Connection_Token"]);
            Console.WriteLine("Active command prefix: " + Midori.MidoriConfig["Command_Prefix"]);
            Console.WriteLine("Accepting bot commands: " + ((bool)Midori.MidoriConfig["AcceptBotCommands"] ? "Yes." : "No."));
            Console.WriteLine("Alerting on unknown command: " + ((bool)Midori.MidoriConfig["AlertOnUnknownCommands"] ? "Yes." : "No."));
            Console.WriteLine("=====");
            await Task.Yield();
        }
    }
}
