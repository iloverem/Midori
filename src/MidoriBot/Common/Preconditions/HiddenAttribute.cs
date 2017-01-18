using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace MidoriBot.Common.Preconditions
{
    public class HiddenAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissions(ICommandContext Context, CommandInfo Command, IDependencyMap Deps)
            => Task.FromResult(PreconditionResult.FromSuccess());
    }
}
