using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using MidoriBot.Common.Preconditions;
using MidoriBot.Common;
using Discord;

namespace MidoriBot.Modules.Owner
{
    [Name("Owner")]
    public class midori_SetGameCommand : ModuleBase
    {
        [Command("SetGame"), Summary("Sets the bots' game."), RequireOwner, MinPermissions(AccessLevel.BotOwner)]
        public async Task SetGameCommand([Remainder]string Game)
        {
            await Midori.MidoriClient.SetGameAsync(Game);
            await ReplyAsync(":information_source: Game changed!");    
        }
    }
}
