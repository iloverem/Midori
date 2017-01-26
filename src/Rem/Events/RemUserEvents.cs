using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using Rem;

namespace Rem.Events
{
    public static class RemUserEvents
    {
        public async static Task UserJoined(SocketGuildUser User)
        {
            SocketChannel Channel = Rem.RemClient.GetChannel(User.Guild.DefaultChannelId);
            await ((Rem.RemClient.GetChannel(User.Guild.DefaultChannelId)) as SocketTextChannel).SendMessageAsync($"Welcome {User.Mention} to **{User.Guild.Name}**!");
        }
        public async static Task UserLeft(SocketGuildUser User)
        {
            SocketChannel Channel = Rem.RemClient.GetChannel(User.Guild.DefaultChannelId);
            await ((Rem.RemClient.GetChannel(User.Guild.DefaultChannelId)) as SocketTextChannel).SendMessageAsync($"Goodbye {User.Mention}! Hopefully they will come back soon.");
        }
    }
}