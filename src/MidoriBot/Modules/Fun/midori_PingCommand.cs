using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class midori_PingCommand : ModuleBase
    {
        [Command("Ping"), Summary("Pong!")]
        public async Task PingCommand()
        {
            await ReplyAsync($"Pong!");
        }
    }
}
