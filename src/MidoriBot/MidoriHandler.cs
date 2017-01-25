using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using MidoriBot.Common;

namespace MidoriBot
{
    public static class MidoriHandler
    {
        private static CommandService MidoriCommands;
        private static DiscordSocketClient MidoriClient;
        private static IDependencyMap MidoriDeps;

        public static async Task Setup(IDependencyMap _MidoriDeps)
        {
            MidoriDeps = _MidoriDeps;
            MidoriClient = MidoriDeps.Get<DiscordSocketClient>();
            MidoriCommands = MidoriDeps.Get<CommandService>();

            await MidoriCommands.AddModulesAsync(Assembly.GetEntryAssembly());

            MidoriClient.MessageReceived += MidoriProcessCommand;
        }

        public static async Task MidoriProcessCommand(SocketMessage Raw)
        {
            SocketUserMessage Message = Raw as SocketUserMessage;
            String Prefix = Midori.MidoriConfig["Command_Prefix"].ToString();

            // Series of checks to make sure we've got a command
            if (Message == null) return;
            if (Message.Content == Prefix) return;
            if (Message.Content.Contains(Prefix + Prefix)) return;
            if (Message.Author.IsBot && ((bool)Midori.MidoriConfig["AcceptBotCommands"])) return;
            if (Message.Author == null) return;

            // Arguments
            int ArgPos = 0;
            if (!(Message.HasMentionPrefix(MidoriClient.CurrentUser, ref ArgPos) || Message.HasStringPrefix(Prefix, ref ArgPos))) return;

            // Preparing for control handover to command
            CommandContext Context = new CommandContext(MidoriClient, Message);
            SearchResult Search = MidoriCommands.Search(Context, ArgPos);
            CommandInfo Command = null;
            if (Search.IsSuccess)
            {
                Command = Search.Commands.FirstOrDefault().Command;
            }

            // Handover
            await Task.Run(async () =>
            {
                IResult Result = await MidoriCommands.ExecuteAsync(Context, ArgPos, MidoriDeps, MultiMatchHandling.Best);

                Logging.LogMessage(LogLevel.Info, LogEvent.Command, $"{Command?.Name ?? Message.Content.Substring(ArgPos).Split(' ').First()} => {(Result.IsSuccess ? Result.ToString() : Result.Error.ToString())} | Server: [{(Context.IsPrivate ? "Private." : Context.Guild.Name)}] | {(Context.IsPrivate ? "Private." : $"Channel: #{Context.Channel.Name} ")} | Author: ({Context.User})");
                if (!Result.IsSuccess)
                {
                    await OnCommandError(Result, Context);
                }
            });
        }

        private static async Task OnCommandError(IResult Search, CommandContext Context)
        {
            ErrorEmbed CommandError = new ErrorEmbed();
            CommandError.Title = "Something didn't work!";
            CommandError.Description = $"I couldn't do what you wanted, {Context.User.Username}!";
            CommandError.ThumbnailUrl = Context.Client.CurrentUser.AvatarUrl;

            // Adding in errors which should not be reported to the bot creator
            Dictionary<CommandError, bool> KnownErrors = new Dictionary<CommandError, bool>
            {
                {Discord.Commands.CommandError.UnmetPrecondition, false },
                {Discord.Commands.CommandError.UnknownCommand, !(bool)Midori.MidoriConfig["AlertOnUnknownCommands"] },
                {Discord.Commands.CommandError.BadArgCount, false }
            };
            // Explanation:
            // I don't want the bot's footer to say "Report this" if it's the users fault.
            // So, in the above dictionary, the bool is whether to say "Report this."
            // the below logic finds the search.error in the dictionary and reflects on sayreport.

            bool SayReport;

            if (KnownErrors.ContainsKey(Search.Error.Value))
            {
                SayReport = KnownErrors[Search.Error.Value];
            }
            else
            {
                SayReport = true;
            }

            // Fields
            CommandError.AddField(Field =>
            {
                Field.IsInline = false;
                Field.Name = "Reason";
                Field.Value = Search.ErrorReason;
            });

            if (Search.Error == Discord.Commands.CommandError.UnknownCommand && !(bool)Midori.MidoriConfig["AlertOnUnknownCommands"] && !Context.IsPrivate)
            {
                SayReport = false;
                return;
            }

            // Footer
            if (SayReport)
            {
                CommandError.WithFooter(Footer =>
                {
                    Footer.Text = $"Please report this to my creator, {MidoriClient.GetUser(MidoriClient.GetApplicationInfoAsync().GetAwaiter().GetResult().Owner.Id).Username}!";
                });
            }

            // Send error message
            await Context.Channel.SendEmbedAsync(CommandError);
        }
    }
}
