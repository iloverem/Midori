using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Rem.Events
{
    public static class RemEvents
    {
        public static void Setup()
        {
            Rem.RemClient.UserJoined += RemUserEvents.UserJoined;
            Rem.RemClient.UserLeft += RemUserEvents.UserLeft;
            Rem.RemClient.Ready += RemReadyEvent.ReadyEvent;
        }
    }
}
