using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rem.Events
{
    public static class RemReadyEvent
    {
        public static async Task ReadyEvent()
        {
            Console.WriteLine("=====");
            Console.WriteLine((Rem.RemClient.GetApplicationInfoAsync().GetAwaiter().GetResult()).Description);
            Console.WriteLine("Active token: " + Rem.RemCredentials["Connection_Token"]);
            Console.WriteLine("Active command prefix: " + Rem.RemConfig["Command_Prefix"]);
            Console.WriteLine("Accepting bot commands: " + ((bool)Rem.RemConfig["AcceptBotCommands"] ? "Yes." : "No."));
            Console.WriteLine("Alerting on unknown command: " + ((bool)Rem.RemConfig["AlertOnUnknownCommands"] ? "Yes." : "No."));
            Console.WriteLine("=====");
            await Task.Yield();
        }
    }
}
