using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Rem.Common.Preconditions;

namespace Rem.Modules.Owner
{
    [Name("Owner")]
    public class RemShutdownCommand : ModuleBase
    {
        [Command("Shutdown"), Alias("Die"), Summary("Turns off the bot."), RequireOwner, MinPermissions(AccessLevel.BotOwner)]
        public async Task DieCommand()
        {
            Console.WriteLine("== !! INITIATED SHUTDOWN !! ==");
            await ReplyAsync(":warning: Shutdown procedure initiated. Goodbye!");
            await Context.Client.DisconnectAsync();
            Context.Client.Dispose();
            Environment.FailFast("Bot safely shutdown.");
            Environment.Exit(1);
        }
    }
}
