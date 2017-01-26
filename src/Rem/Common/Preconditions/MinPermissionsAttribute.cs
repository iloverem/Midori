using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Rem;
using Discord;

namespace Rem.Common.Preconditions
{
    // Sets the minimum permission required to use a module/command
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MinPermissionsAttribute : PreconditionAttribute
    {
        AccessLevel Level;
        public MinPermissionsAttribute(AccessLevel _Level)
        {
            Level = _Level;
        }

        public override Task<PreconditionResult> CheckPermissions(ICommandContext Context, CommandInfo Command, IDependencyMap Deps)
        {
            AccessLevel Access = GetPermission(Context);

            if (Access >= Level)
                return Task.FromResult(PreconditionResult.FromSuccess());
            else
                return Task.FromResult(PreconditionResult.FromError(":warning: Sir, I can't let you do that... [Invalid Permissions]"));
        }

        public AccessLevel GetPermission(ICommandContext Context)
        {
            if (Context.User.IsBot && !(bool)Rem.RemConfig["AcceptBotCommands"])
                return AccessLevel.Blocked; // No bots

            IApplication CurrentApp = Context.Client.GetApplicationInfoAsync().Result;

            if (CurrentApp.Owner.Id == Context.User.Id)
                return AccessLevel.BotOwner;

            SocketGuildUser User = Context.User as SocketGuildUser;
            if (User != null)
            {
                if (Context.Guild.OwnerId == User.Id)
                    return AccessLevel.ServerOwner;

                if (User.GuildPermissions.ManageMessages ||
                    User.GuildPermissions.BanMembers ||
                    User.GuildPermissions.KickMembers)
                    return AccessLevel.ServerModerator;

                // TO-DO: Implement Trusted role system?
                return AccessLevel.User;
            }
            return AccessLevel.IsPrivate;
        }
    }
}
