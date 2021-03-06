﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Text;
using Rem.Common;

namespace Rem.Modules.Owner
{
    [Name("Owner")]
    public class RemOwnerInfoCommand : ModuleBase
    {
        [Command("OwnerInfo"), Summary("Gets secret information about me!"), RequireOwner]
        public async Task OwnerInfoCommand()
        {
            NormalEmbed Embed = new NormalEmbed();
            StringBuilder sb = new StringBuilder();
            Embed.Title = "Owner Information";
            sb.AppendLine("Current token: " + Rem.RemCredentials["Connection_Token"]);
            Embed.Description = sb.ToString();
            await (await (Context.User.CreateDMChannelAsync())).SendEmbedAsync(Embed);
        }
    }
}
