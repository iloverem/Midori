using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using MidoriBot.Common;
using Discord;

namespace MidoriBot.Modules.Info
{
    [Name("Info")]
    public class midori_AvatarCommand : ModuleBase
    {
        [Command("Avatar"), Summary("Gets the avatar for a user.")]
        public async Task AvatarCommand(SocketGuildUser TargetUser = null)
        {
            if (TargetUser == null) TargetUser = (Context.User as SocketGuildUser);
            NormalEmbed AvatarEmbed = new NormalEmbed();
            AvatarEmbed.Title = TargetUser.GetEffectiveName();
            AvatarEmbed.ImageUrl = TargetUser.AvatarUrl;
            AvatarEmbed.WithAuthor((new EmbedAuthorBuilder()).WithIconUrl(TargetUser.AvatarUrl));
            await Context.Channel.SendEmbedAsync(AvatarEmbed);
        }
    }
}
