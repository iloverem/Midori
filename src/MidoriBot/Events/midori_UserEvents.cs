using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;

namespace MidoriBot.Events
{
    public class midori_UserEvents
    {
        private DiscordSocketClient Client;
        public midori_UserEvents(IDependencyMap Map)
        {
            Client = Map.Get<DiscordSocketClient>();
        }
        public async Task UserJoined(SocketGuildUser User)
        {
            SocketChannel Channel = Client.GetChannel(User.Guild.DefaultChannelId);
            await ((Client.GetChannel(User.Guild.DefaultChannelId)) as SocketTextChannel).SendMessageAsync($"Welcome {User.Mention} to **{User.Guild.Name}**!");
        }
        public async Task UserLeft(SocketGuildUser User)
        {
            SocketChannel Channel = Client.GetChannel(User.Guild.DefaultChannelId);
            await ((Client.GetChannel(User.Guild.DefaultChannelId)) as SocketTextChannel).SendMessageAsync($"Goodbye {User.Mention}! Hopefully they will come back soon.");
        }
    }
}
