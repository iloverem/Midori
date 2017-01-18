using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace MidoriBot.Common
{
    public class ErrorEmbed : EmbedBase
    {
        public ErrorEmbed()
        {
            Color = new Color(255, 0, 0);
        }
    }

    public class MEmbedFooter : EmbedFooterBuilder
    {
        public MEmbedFooter(IDiscordClient Client)
        {
            WithIconUrl(Client.CurrentUser.AvatarUrl);
        }
    }

    public class EmbedBase : EmbedBuilder
    {
        public EmbedBase()
        {
            
        }
    }

    public class NormalEmbed : EmbedBase
    {
        public NormalEmbed(byte Red = 0, byte Green = 0, byte Blue = 255)
        {
            Color = new Color(0, 0, 250);
        }
    }
}
