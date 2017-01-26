using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace Rem.Modules.Fun
{
    [Name("Fun")]
    public class RemPingCommand : ModuleBase
    {
        [Command("Ping"), Summary("Pong!")]
        public async Task PingCommand()
        {
            await ReplyAsync($"Pong!");
        }
    }
}
