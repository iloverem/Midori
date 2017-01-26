using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using Rem.Common;

namespace Rem.Modules.Info
{
    [Name("Info")]
    public class RemServerInfoCommand : ModuleBase
    {
        [Command("ServerInfo"), Alias("SInfo"), Summary("Gets information for the current server.")]
        public async Task ServerInfoCommand()
        {
            SocketGuild Guild = Context.Guild as SocketGuild;
            NormalEmbed Embed = new NormalEmbed();
            Embed.Title = $"Server {Guild.Name}";
            Embed.ThumbnailUrl = Guild.IconUrl;
            int TotalRoles = Guild.Roles.Count;
            string VoiceRegion = (await (Context.Client.GetVoiceRegionAsync(Guild.VoiceRegionId))).Name;
            string VerificationLevel = Guild.VerificationLevel.ToString();
            int VoiceChannels = (await (Guild.GetVoiceChannelsAsync())).Count();
            int TextChannels = (await Guild.GetTextChannelsAsync()).Count();
            string Members = $"{(Guild.Users.Where(User => (User.Status == UserStatus.Online))).Count()} Online / {Guild.Users.Count.ToString()} Total";
            string Owner = (await Context.Client.GetUserAsync(Guild.OwnerId)).Username;

            Embed.AddField(x =>
            {
                x.Name = "Roles";
                x.Value = TotalRoles.ToString();
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Name = "Voice Region";
                x.IsInline = true;
                x.Value = VoiceRegion;
            });
            Embed.AddField(x =>
            {
                x.Name = "Verification Level";
                x.Value = VerificationLevel;
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Value = VoiceChannels.ToString();
                x.IsInline = true;
                x.Name = "Voice Channels";
            });
            Embed.AddField(x =>
            {
                x.Name = "Text Channels";
                x.Value = TextChannels.ToString();
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Value = Members;
                x.Name = "Total Members";
                x.IsInline = true;
            });
            Embed.AddField(x =>
            {
                x.Name = "Owner";
                x.Value = Owner;
                x.IsInline = true;
            });
            Embed.Description = $"{Guild.Name} is a Discord server with {Guild.Users.Count} {(Guild.Users.Count > 1 ? "members" : "member")}, with {(Guild.Users.Where(User => (User.Status == UserStatus.Online))).Count()} online. It has {VoiceChannels} voice {(VoiceChannels > 1 ? "channels": "channel")}, {TextChannels} text channels, a voice region of {VoiceRegion} and {TotalRoles} {(TotalRoles > 1 ? "roles" : "role")}. It is being operated by {Owner}. If you want to chat here, you're going to need Verification Level {VerificationLevel}.";
            Embed.Footer = (new MEmbedFooter()).WithText($"Server ID: {Guild.Id}");
            await Context.Channel.SendEmbedAsync(Embed);
        }
    }
}
