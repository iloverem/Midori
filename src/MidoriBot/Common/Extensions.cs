using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;

namespace MidoriBot.Common
{
    public static class Extensions
    {
        public static async Task<IEnumerable<CommandInfo>> CheckConditions(this IEnumerable<CommandInfo> Commands, CommandContext Context, IDependencyMap Deps = null)
        {
            List<CommandInfo> Ret = new List<CommandInfo>();
            foreach (CommandInfo Command in Commands)
            {
                if ((await Command.CheckPreconditionsAsync(Context, Deps)).IsSuccess)
                {
                    Ret.Add(Command);
                }
                else
                {
                    PreconditionAttribute RequireContext = Command.Preconditions.FirstOrDefault(a => a is RequireContextAttribute);
                    if (Command.Preconditions.Contains(RequireContext))
                    {
                        Ret.Add(Command);
                    }
                }
            }
            return Ret;
        }
        public static async Task<IUserMessage> SendEmbedAsync(this IMessageChannel Channel, EmbedBuilder Embed) =>
            await Channel.SendMessageAsync("", false, Embed);
        public static bool GetBooleanFromString(string Boolean)
        {
            if (Boolean.ToUpper() == "true") return true;
            else return false;
        }
        public static string GetEffectiveName(this SocketGuildUser TargetUser)
        {
            if (TargetUser.Nickname != null)
            {
                return TargetUser.Nickname;
            }
            else
            {
                return TargetUser.Username;
            }
        }
    }
}
