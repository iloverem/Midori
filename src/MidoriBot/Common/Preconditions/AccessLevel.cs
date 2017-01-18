using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidoriBot.Common.Preconditions
{
    public enum AccessLevel
    {
        Blocked,
        IsPrivate,
        User,
        Trusted,
        ServerModerator,
        ServerOwner,
        BotOwner
    }
}
