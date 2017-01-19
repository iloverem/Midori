using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace MidoriBot.Modules.Owner
{
    [Name("Owner")]
    public class midori_LeaveCommand : ModuleBase
    {
        [Command("Leave"), Alias("LeaveServer"), Summary("The bot will leave the current server."), RequireContext(ContextType.Guild), RequireOwner]
        public async Task LeaveCommand()
        {
            await ReplyAsync("My time is up in this server. Goodbye!");
            await Context.Guild.LeaveAsync();
        }
    }
}
