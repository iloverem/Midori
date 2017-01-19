using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using MidoriBot.Common.Preconditions;
using Discord.WebSocket;
using MidoriBot.Common;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class midori_ShipCommand : ModuleBase
    {
        [Command("Ship"), Alias("OneTruePair", "OTP"), Summary("Shows who are meant for each other."), MinPermissions(AccessLevel.User)]
        public async Task ShipCommand()
        {
            Random RandomClient = new Random();
            int GuildMemberCount = (Context.Guild as SocketGuild).Users.Count;
            int RandomMember1 = RandomClient.Next(1, GuildMemberCount);
            int RandomMember2 = RandomClient.Next(1, GuildMemberCount);
            if (RandomMember1 == RandomMember2)
            {
                int Random = RandomClient.Next(1, 2);
                if (Random == 1)
                {
                    RandomMember1 = RandomClient.Next(1, GuildMemberCount);
                }
                else
                {
                    RandomMember2 = RandomClient.Next(1, GuildMemberCount);
                }
            }
            await ReplyAsync($"<3 `{Extensions.GetEffectiveName(((Context.Guild as SocketGuild).Users.ElementAt(RandomMember1)))}` x `{((Extensions.GetEffectiveName((Context.Guild as SocketGuild).Users.ElementAt(RandomMember2))))}` <3");
        }
    }
}
