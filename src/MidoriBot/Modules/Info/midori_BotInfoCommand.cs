using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MidoriBot.Common;
using MidoriBot.Common.Preconditions;
using Discord.Commands;
using Discord;

namespace MidoriBot.Modules.Info
{
    [Name("Info")]
    public class midori_BotInfoCommand : ModuleBase
    {
        [Command("botinfo"), Alias("BInfo"), Summary("Information about me!"), MinPermissions(AccessLevel.User)]
        public async Task Midori_BotInfoCommand()
        {
            NormalEmbed Info = new NormalEmbed();
            Info.Title = "Bot Information";
            Info.Description = MidoriConfig.BotDescription;
            Info.AddField(MD =>
            {
                MD.Name = "Bot Version";
                MD.Value = MidoriConfig.MidoriVersion;
            });
            Info.AddField(MD =>
            {
                MD.Name = "Owner";
                MD.Value = Context.Client.GetUserAsync(Context.Client.GetApplicationInfoAsync().Result.Owner.Id).Result.Username;
            });
            Info.Footer = (new MEmbedFooter(Context.Client)).WithText("Bot Information");
            await Context.Channel.SendEmbedAsync(Info);
        }
    }
}
