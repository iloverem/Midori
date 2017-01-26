using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Rem.Common.Preconditions;

namespace Rem.Modules.Owner
{
    [Name("Owner")]
    public class RemLeaveCommand : ModuleBase
    {
        [Command("Leave"), Alias("LeaveServer"), Summary("The bot will leave the current server."), RequireContext(ContextType.Guild), MinPermissions(AccessLevel.BotOwner)]
        public async Task LeaveCommand()
        {
            await ReplyAsync("My time is up in this server. Goodbye!");
            await Context.Guild.LeaveAsync();
        }
    }
}
