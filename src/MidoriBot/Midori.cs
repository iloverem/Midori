using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;

namespace MidoriBot
{
    public class Midori
    {
        public DiscordSocketClient MidoriClient;
        public DiscordSocketConfig MidoriSocketConfig = new DiscordSocketConfig
        {
            DownloadUsersOnGuildAvailable = true,
            LogLevel = LogSeverity.Info,
            MessageCacheSize = 10
        };
        public CommandService MidoriCommands;
        public CommandServiceConfig MidoriCommandsConfig = new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Sync
        };
        public MidoriHandler CommandHandler;

        public static void Main(string[] args)
        {
            Midori MidoriBot = new Midori();
            MidoriBot.Start().GetAwaiter().GetResult();
        }

        public async Task Start()
        {
            MidoriClient = new DiscordSocketClient(MidoriSocketConfig);
            MidoriCommands = new CommandService(MidoriCommandsConfig);
            CommandHandler = new MidoriHandler();

            // Setup dependencies
            IDependencyMap MidoriDeps = new DependencyMap();
            MidoriDeps.Add(MidoriClient);
            MidoriDeps.Add(MidoriCommands);

            // Events handler
            // TO-DO: Make an events handler

            // Login and connect
            await MidoriClient.LoginAsync(TokenType.Bot, MidoriConfig.ConnectionToken);
            await MidoriClient.ConnectAsync();
            await MidoriClient.DownloadAllUsersAsync();

            // Install command handling
            await CommandHandler.Setup(MidoriDeps);

            // Keep the bot running
            await Task.Delay(-1);
        }
    }
}
