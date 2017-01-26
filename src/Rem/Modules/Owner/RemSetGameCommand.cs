using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Rem.Common.Preconditions;
using Rem.Common;
using Discord;

namespace Rem.Modules.Owner
{
    [Name("Owner")]
    public class RemSetGameCommand : ModuleBase
    {
        [Command("SetGame"), Summary("Sets the bots' game."), RequireOwner, MinPermissions(AccessLevel.BotOwner)]
        public async Task SetGameCommand([Remainder]string Game)
        {
            await Rem.RemClient.SetGameAsync(Game);
            await ReplyAsync(":information_source: Game changed!");    
        }
    }
}
