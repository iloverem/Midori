using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using MidoriBot.Common;
using MidoriBot.Common.Preconditions;
using Discord.WebSocket;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class midori_RandomUserCommand : ModuleBase
    {
        [Command("Random"), Alias("Randu"), Summary("Gets a random user from the current server.")]
        [MinPermissions(AccessLevel.User)]
        public async Task RandomUserCommand()
        {
            SocketGuild TargetGuild = Context.Guild as SocketGuild;
            Random Generator = new Random();
            SocketGuildUser FoundUser = TargetGuild.Users.ElementAt(Generator.Next(1, TargetGuild.Users.Count));
            NormalEmbed RandomUser = new NormalEmbed();
            RandomUser.Title = "The wise Midori has picked...";
            RandomUser.Description = FoundUser.Username;
            await Context.Channel.SendEmbedAsync(RandomUser);
        }
    }
}
