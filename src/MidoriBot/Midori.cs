using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using MidoriBot.Events;
using System.Net.Http;

namespace MidoriBot
{
    public class Midori
    {
        public static DiscordSocketClient MidoriClient;
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
            try
            {
                MidoriBot.Start().GetAwaiter().GetResult();
                Console.WriteLine("Handing over control to async...");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Cannot connect to Discord! D:");
                Console.WriteLine($"Exception details:");
                Console.WriteLine(e);
            }
        }



        public async Task Start()
        {
            MidoriClient = new DiscordSocketClient(MidoriSocketConfig);
            MidoriCommands = new CommandService(MidoriCommandsConfig);
            CommandHandler = new MidoriHandler();
            Console.WriteLine("Created client, command service and command handler.");

            // Setup dependencies
            IDependencyMap MidoriDeps = new DependencyMap();
            MidoriDeps.Add(MidoriClient);
            MidoriDeps.Add(MidoriCommands);
            Console.WriteLine("Organized dependency library.");

            // Events handler
            MidoriEvents MidoriEvents = new MidoriEvents();
            MidoriEvents.Install();
            Console.WriteLine("Installed event handler.");

            // Login and connect
            await MidoriClient.LoginAsync(TokenType.Bot, MidoriConfig.ConnectionToken);
            Console.WriteLine("Sent login information to Discord.");
            await MidoriClient.ConnectAsync();
            Console.WriteLine("Connected.");
            await MidoriClient.DownloadAllUsersAsync();

            // Install command handling
            await CommandHandler.Setup(MidoriDeps);
            Console.WriteLine("Installed commands handler.");

            // Keep the bot running
            await Task.Delay(-1);
        }
    }
}
