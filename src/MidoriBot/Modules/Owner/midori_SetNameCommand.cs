using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using MidoriBot.Common.Preconditions;

namespace MidoriBot.Modules.Owner
{
    [Name("Owner")]
    public class midori_SetNameCommand : ModuleBase
    {
        [Command("SetName"), Summary("Changes the bot name."), RequireOwner, MinPermissions(AccessLevel.BotOwner)]
        public async Task SetNameCommand([Remainder, Summary("New name.")] string NewName)
        {
            await Midori.MidoriClient.CurrentUser.ModifyAsync(x =>
            {
                x.Username = NewName;
            });
        }
    }
}
