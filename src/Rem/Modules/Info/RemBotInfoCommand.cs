using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rem.Common;
using Rem.Common.Preconditions;
using Discord.Commands;
using Discord;

namespace Rem.Modules.Info
{
    [Name("Info")]
    public class RemBotInfoCommand : ModuleBase
    {
        [Command("BotInfo"), Alias("BInfo"), Summary("Information about me!"), MinPermissions(AccessLevel.User)]
        public async Task BotInfoCommand()
        {
            NormalEmbed Info = new NormalEmbed();
            Info.Title = "Bot Information";
            Info.Description = Rem.GetDescription();
            Info.ThumbnailUrl = Context.Client.CurrentUser.AvatarUrl;
            Info.AddField(MD =>
            {
                MD.Name = "Bot Version";
                MD.Value = Rem.Version;
                MD.IsInline = true;
            });
            Info.AddField(MD =>
            {
                MD.Name = "Owner";
                MD.Value = Context.Client.GetUserAsync(Context.Client.GetApplicationInfoAsync().GetAwaiter().GetResult().Owner.Id).GetAwaiter().GetResult().Username;
                MD.IsInline = true;
            });
            Info.AddField(x =>
            {
                x.Name = "Discriminator";
                x.Value = Context.Client.CurrentUser.Discriminator;
            });
            Info.AddField(x =>
            {
                x.Name = "Game";
                if (Context.Client.CurrentUser.Game.HasValue)
                    x.Value = "Playing " + Context.Client.CurrentUser.Game.Value;
                else
                    x.Value = "Not playing anything.";
            });
            Info.AddField(x =>
            {
                x.Name = "Status";
                x.Value = "Happy serving commands!";
            });
            Info.AddField(x =>
            {
                x.Name = "Language";
                x.Value = "C#, proudly using .NET Core.";
            });
            Info.AddField(x =>
            {
                x.Name = "Total Servers";
                x.Value = Context.Client.GetGuildsAsync().GetAwaiter().GetResult().Count.ToString();
            });

            Info.Footer = (new MEmbedFooter()).WithText("Bot Information");
            await Context.Channel.SendEmbedAsync(Info);
        }
    }
}
