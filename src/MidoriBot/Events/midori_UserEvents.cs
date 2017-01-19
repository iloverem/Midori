using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using MidoriBot;

namespace MidoriBot.Events
{
    public static class midori_UserEvents
    {
        public async static Task UserJoined(SocketGuildUser User)
        {
            SocketChannel Channel = Midori.MidoriClient.GetChannel(User.Guild.DefaultChannelId);
            await ((Midori.MidoriClient.GetChannel(User.Guild.DefaultChannelId)) as SocketTextChannel).SendMessageAsync($"Welcome {User.Mention} to **{User.Guild.Name}**!");
        }
        public async static Task UserLeft(SocketGuildUser User)
        {
            SocketChannel Channel = Midori.MidoriClient.GetChannel(User.Guild.DefaultChannelId);
            await ((Midori.MidoriClient.GetChannel(User.Guild.DefaultChannelId)) as SocketTextChannel).SendMessageAsync($"Goodbye {User.Mention}! Hopefully they will come back soon.");
        }
    }
}