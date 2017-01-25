using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Text;
using Discord.WebSocket;

namespace MidoriBot.Modules.Owner
{
    [Name("Owner")]
    public class midori_Servers : ModuleBase
    {
        [Command("Servers"), Summary("See the servers I'm in.")]
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
