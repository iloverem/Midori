using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class midori_SayCommand : ModuleBase
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
