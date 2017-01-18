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
    public class MidoriHandler
    {
        private CommandService MidoriCommands;
        private DiscordSocketClient MidoriClient;
        private IDependencyMap MidoriDeps;

        public async Task Setup(IDependencyMap _MidoriDeps)
        {
            MidoriDeps = _MidoriDeps;
            MidoriClient = MidoriDeps.Get<DiscordSocketClient>();
            MidoriCommands = MidoriDeps.Get<CommandService>();

            await MidoriCommands.AddModulesAsync(Assembly.GetEntryAssembly());

            MidoriClient.MessageReceived += MidoriProcessCommand;
        }

        public async Task MidoriProcessCommand(SocketMessage Raw)
        {
            SocketUserMessage Message = Raw as SocketUserMessage;
            String Prefix = MidoriConfig.CommandPrefix;

            // Series of checks to make sure we've got a command
            if (Message == null) return;
            if (Message.Content == Prefix) return;
            if (Message.Content.Contains(Prefix + Prefix)) return;
            if (Message.Author.IsBot && !MidoriConfig.AcceptBotCommands) return;
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

                Logging.LogMessage(LogLevel.Info, LogEvent.Command, $"{Command?.Name ?? Message.Content.Substring(ArgPos).Split(' ').First()} => {(Result.IsSuccess ? Result.ToString() : Result.Error.ToString())} | Server: [{(Context.IsPrivate ? "Private." : Context.Guild.Name)}] | {(Context.IsPrivate ? "Private." : $"Channel: #{Context.Channel.Name} ")} | Author: ({Context.User}) || {Message.Content.Substring(Message.Content.Split(' ').First().Length)}");
                if (!Result.IsSuccess)
                {
                    await OnCommandError(Result, Context);
                }
            });
        }

        private async Task OnCommandError(IResult Search, CommandContext Context)
        {
            ErrorEmbed CommandError = new ErrorEmbed();
            CommandError.Title = "Something didn't work!";
            CommandError.Description = $"I couldn't do what you wanted, {Context.User.Username}! Gomen nasai!";
            CommandError.ThumbnailUrl = Context.Client.CurrentUser.AvatarUrl;

            // Unknown command
            if ((Search.Error == Discord.Commands.CommandError.UnknownCommand && !MidoriConfig.AlertOnUnknownCommand)) return;

            // Fields
            CommandError.AddField(Field =>
            {
                Field.IsInline = false;
                Field.Name = "Reason";
                Field.Value = Search.ErrorReason;
            });

            // Footer
            CommandError.WithFooter(Footer =>
            {
                Footer.Text = $"Please report this to my master, {MidoriClient.GetUser(MidoriClient.GetApplicationInfoAsync().Result.Owner.Id).Username}!";
            });

            // Send error message
            await Context.Channel.SendMessageAsync("", false, CommandError);
        }
    }
}
