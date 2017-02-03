using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Rem.Common.Preconditions;

namespace Rem.Modules.Owner
{
    [Name("Owner")]
    public class RemSetNameCommand : ModuleBase
    {
        [Command("SetName"), Summary("Changes the bot name."), RequireOwner, MinPermissions(AccessLevel.BotOwner)]
        public async Task SetNameCommand([Remainder, Summary("New name.")] string NewName)
        {
            try
            {
                await Rem.RemClient.CurrentUser.ModifyAsync(x =>
                {
                    x.Username = NewName;
                });
                await ReplyAsync(":information_source: Username changed to " + NewName + "!");
            }
            catch
            {
                await ReplyAsync(":warning: Failed to change username!");
            }
        }
    }
}
