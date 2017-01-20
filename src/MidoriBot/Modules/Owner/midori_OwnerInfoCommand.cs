using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Text;
using MidoriBot.Common;

namespace MidoriBot.Modules.Owner
{
    [Name("Owner")]
    public class midori_OwnerInfoCommand : ModuleBase
    {
        [Command("OwnerInfo"), Summary("Gets secret information about me!"), RequireOwner]
        public async Task OwnerInfoCommand()
        {
            NormalEmbed Embed = new NormalEmbed();
            StringBuilder sb = new StringBuilder();
            Embed.Title = "Owner Information";
            sb.AppendLine("Current token: " + Midori.MidoriCredentials["Connection_Token"]);
            Embed.Description = sb.ToString();
            await (Context.User.CreateDMChannelAsync().GetAwaiter().GetResult()).SendEmbedAsync(Embed);
        }
    }
}
