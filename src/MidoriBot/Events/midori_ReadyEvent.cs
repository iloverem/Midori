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
            Console.WriteLine(MidoriConfig.GetDescription());
            Console.WriteLine("Active token: " + MidoriConfig.ConnectionToken);
            Console.WriteLine("Active command prefix: " + MidoriConfig.CommandPrefix);
            Console.WriteLine("Accepting bot commands: " + (MidoriConfig.AcceptBotCommands ? "Yes." : "No."));
            Console.WriteLine("Alerting on unknown command: " + (MidoriConfig.AlertOnUnknownCommand ? "Yes." : "No."));
        }
    }
}
