using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace Rem.Modules.Fun
{
    [Name("Fun")]
    public class RemSayCommand : ModuleBase
    {
        [Command("Say"), Summary("Says the input.")]
        public async Task SayCommand([Remainder] string WhatToSay)
        {
            if ((Context.Guild.GetCurrentUserAsync().GetAwaiter().GetResult()).GuildPermissions.ManageMessages)
            {
                await Context.Message.DeleteAsync();
            }
            await ReplyAsync(WhatToSay);
        }
    }
}
