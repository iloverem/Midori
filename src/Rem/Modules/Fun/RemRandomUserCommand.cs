using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Rem.Common;
using Rem.Common.Preconditions;
using Discord.WebSocket;

namespace Rem.Modules.Fun
{
    [Name("Fun")]
    public class RemRandomUserCommand : ModuleBase
    {
        [Command("Random"), Alias("Randu"), Summary("Gets a random user from the current server.")]
        [MinPermissions(AccessLevel.User)]
        public async Task RandomUserCommand()
        {
            SocketGuild TargetGuild = Context.Guild as SocketGuild;
            Random Generator = new Random();
            SocketGuildUser FoundUser = TargetGuild.Users.ElementAt(Generator.Next(1, TargetGuild.Users.Count));
            NormalEmbed RandomUser = new NormalEmbed();
            RandomUser.Title = "The wise Rem has picked...";
            RandomUser.Description = FoundUser.Username;
            await Context.Channel.SendEmbedAsync(RandomUser);
        }
    }
}
