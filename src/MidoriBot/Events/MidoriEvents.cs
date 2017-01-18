using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace MidoriBot.Events
{
    public class MidoriEvents
    {
        private DiscordSocketClient MidoriClient;
        private IDependencyMap Map;
        public MidoriEvents(IDependencyMap _Map)
        {
            Map = _Map;
            MidoriClient = Map.Get<DiscordSocketClient>();
        }
        
        public void Install()
        {
            midori_UserEvents UserEventManager = new midori_UserEvents(Map);
            MidoriClient.UserJoined += UserEventManager.UserJoined;
            MidoriClient.UserLeft += UserEventManager.UserLeft;
        }
    }
}
