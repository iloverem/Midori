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
            Info.Title = "Midori Information";
            Info.Description = MidoriConfig.BotDescription;
            Info.AddField(MD =>
            {
                MD.Name = "Midori Version";
                MD.Value = MidoriConfig.MidoriVersion;
            });
            Info.AddField(MD =>
            {
                MD.Name = "Owner";
                MD.Value = Context.Client.GetUserAsync(Context.Client.GetApplicationInfoAsync().Result.Owner.Id).Result.Username;
            });
            Info.AddField(MD =>
            {
                MD.Name = "Creation Date";
                DateTimeOffset CreatedAt = Context.Client.CurrentUser.CreatedAt;
                Dates DateConfig = new Dates();
                DateConfig.Setup();
                MD.Value = DateConfig.GetDate(CreatedAt);
            });
            Info.Footer = (new EmbedFooterBuilder()).WithText("Midori Bot Information");
            await Context.Channel.SendEmbedAsync(Info);
        }
    }
}
