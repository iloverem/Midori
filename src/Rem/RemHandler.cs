using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using Rem.Common;

namespace Rem
{
    public static class RemHandler
    {
        private static CommandService RemService;
        private static DiscordSocketClient RemClient;
        private static IDependencyMap RemDeps;

        public static async Task Setup(IDependencyMap _RemDeps)
        {
            RemDeps = _RemDeps;
            RemClient = RemDeps.Get<DiscordSocketClient>();
            RemService = RemDeps.Get<CommandService>();

            await RemService.AddModulesAsync(Assembly.GetEntryAssembly());

            RemClient.MessageReceived += HandleCommand;
        }

        public static async Task HandleCommand(SocketMessage Raw)
        {
            SocketUserMessage Message = Raw as SocketUserMessage;
            String Prefix = Rem.RemConfig["Command_Prefix"].ToString();

            // Series of checks to make sure we've got a command
            if (Message == null) return;
            if (Message.Content == Prefix) return;
            if (Message.Content.Contains(Prefix + Prefix)) return;
            if (Message.Author.IsBot && ((bool)Rem.RemConfig["AcceptBotCommands"])) return;
            if (Message.Author == null) return;

            // Arguments
            int ArgPos = 0;
            if (!(Message.HasMentionPrefix(RemClient.CurrentUser, ref ArgPos) || Message.HasStringPrefix(Prefix, ref ArgPos))) return;

            // Preparing for control handover to command
            CommandContext Context = new CommandContext(RemClient, Message);
            SearchResult Search = RemService.Search(Context, ArgPos);
            CommandInfo Command = null;
            if (Search.IsSuccess)
            {
                Command = Search.Commands.FirstOrDefault().Command;
            }

            // Handover
            await Task.Run(async () =>
            {
                IResult Result = await RemService.ExecuteAsync(Context, ArgPos, RemDeps, MultiMatchHandling.Best);

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
                {Discord.Commands.CommandError.UnknownCommand, !(bool)Rem.RemConfig["AlertOnUnknownCommands"] },
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

            if (Search.Error == Discord.Commands.CommandError.UnknownCommand && !(bool)Rem.RemConfig["AlertOnUnknownCommands"] && !Context.IsPrivate)
            {
                SayReport = false;
                return;
            }

            // Footer
            if (SayReport)
            {
                CommandError.WithFooter(Footer =>
                {
                    Footer.Text = $"Please report this to my creator, {RemClient.GetUser(RemClient.GetApplicationInfoAsync().GetAwaiter().GetResult().Owner.Id).Username}!";
                });
            }

            // Send error message
            await Context.Channel.SendEmbedAsync(CommandError);
        }
    }
}
