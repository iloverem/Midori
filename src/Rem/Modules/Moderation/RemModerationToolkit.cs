﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using Rem.Common;

namespace Rem.Modules.Moderation
{
    [Name("Moderation")]
    public class RemModerationToolkit : ModuleBase
    {
        [Command("Mute"), Summary("Mutes a member."), RequireUserPermission(GuildPermission.ManageRoles), RequireContext(ContextType.Guild), RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task MuteCommand(SocketGuildUser TargetUser)
        {
            SocketRole MutedRole = TargetUser.Guild.Roles.First(x => x.Name == "Muted");
            if (TargetUser.RoleIds.Contains(MutedRole.Id))
            {
                await ReplyAsync($"{TargetUser.Mention} is already muted.");
                return;
            }
            await TargetUser.AddRolesAsync(MutedRole);
            await ReplyAsync($"{TargetUser.Mention} has been muted.");
        }
        [Command("Unmute"), Summary("Unmutes a member."), RequireUserPermission(GuildPermission.ManageRoles), RequireContext(ContextType.Guild), RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task UnmuteCommand(SocketGuildUser TargetUser)
        {
            SocketRole MutedRole = TargetUser.Guild.Roles.First(x => x.Name == "Muted");
            if (TargetUser.RoleIds.Contains(MutedRole.Id))
            {
                await TargetUser.RemoveRolesAsync(MutedRole);
                await ReplyAsync($"{TargetUser.Mention} has been unmuted!");
            }
            else
            {
                await ReplyAsync($"{TargetUser.Mention} is not muted.");
            }
        }
        [Command("Ban"), Summary("Bans a member."), RequireUserPermission(GuildPermission.BanMembers), RequireContext(ContextType.Guild), RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanCommand(SocketGuildUser TargetUser)
        {
            await TargetUser.Guild.AddBanAsync(TargetUser);
        }
    }
}
