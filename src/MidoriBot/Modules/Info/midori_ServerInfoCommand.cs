using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using MidoriBot.Common;

namespace MidoriBot.Modules.Info
{
    [Name("Info")]
    public class midori_ServerInfoCommand : ModuleBase
    {
        [Command("ServerInfo"), Alias("SInfo"), Summary("Gets information for the current server.")]
        public async Task ServerInfoCommand()
        {
            SocketGuild Guild = Context.Guild as SocketGuild;
            NormalEmbed Embed = new NormalEmbed();
            Embed.Title = $"Information for server {Guild.Name}";
            Embed.ThumbnailUrl = Guild.IconUrl;
            Embed.AddField(x =>
            {
                x.Name = "Roles";
                x.Value = Guild.Roles.Count.ToString();
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Name = "Verification Level";
                x.Value = Guild.VerificationLevel.ToString();
                x.IsInline = true;
            });
            /*Embed.AddField(x =>
            {
                x.Name = "Date Created";
                Dates DateGen = new Dates();
                DateGen.Setup();
                x.Value = DateGen.GetDate(Guild.CreatedAt);
            });*/
            Embed.AddField(x =>
            {
                x.Value = Guild.GetVoiceChannelsAsync().Result.Count().ToString();
                x.IsInline = true;
                x.Name = "Voice Channels";
            });
            Embed.AddField(x =>
            {
                x.Name = "Text Channels";
                x.Value = Guild.GetTextChannelsAsync().Result.Count().ToString();
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Value = Guild.Users.Count.ToString();
                x.Name = "Total Members";
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Name = "Owner";
                x.Value = Context.Client.GetUserAsync(Guild.OwnerId).Result.Username;
                x.IsInline = true;
            });
            await Context.Channel.SendEmbedAsync(Embed);
        }
    }
}
