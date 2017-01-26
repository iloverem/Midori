using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Text;
using Discord.WebSocket;
using Rem.Common.Preconditions;

namespace Rem.Modules.Owner
{
    [Name("Owner")]
    public sealed class RemServers : ModuleBase
    {
        [Command("Servers"), Summary("See the servers I'm in."), MinPermissions(AccessLevel.BotOwner)]
        public async Task ServersCommand()
        {
            StringBuilder s = new StringBuilder();
            int Pos = 1;
            foreach (SocketGuild Guild in (await Context.Client.GetGuildsAsync()))
            {
                s.AppendLine($"{Pos}. **{Guild.Name}** (ID: {Guild.Id})");
                Pos += 1;
            }
            await ReplyAsync(s.ToString());
        }
    }
}
