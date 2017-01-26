using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Rem.Common;
using Discord;

namespace Rem.Modules.Info
{
    [Name("Info")]
    public class RemAvatarCommand : ModuleBase
    {
        [Command("Avatar"), Summary("Gets the avatar for a user.")]
        public async Task AvatarCommand(SocketGuildUser TargetUser = null)
        {
            if (TargetUser == null) TargetUser = (Context.User as SocketGuildUser);
            NormalEmbed AvatarEmbed = new NormalEmbed();
            AvatarEmbed.Title = TargetUser.GetEffectiveName();
            AvatarEmbed.ImageUrl = TargetUser.AvatarUrl;
            await Context.Channel.SendEmbedAsync(AvatarEmbed);
        }
    }
}
