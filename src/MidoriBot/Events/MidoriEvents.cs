using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace MidoriBot.Events
{
    public static class MidoriEvents
    {
        public static void Setup()
        {
            Midori.MidoriClient.UserJoined += midori_UserEvents.UserJoined;
            Midori.MidoriClient.UserLeft += midori_UserEvents.UserLeft;
            Midori.MidoriClient.Ready += midori_ReadyEvent.ReadyEvent;
        }
    }
}
