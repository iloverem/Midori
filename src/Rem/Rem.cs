using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using Rem.Events;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Rem
{
    public sealed class Rem
    {
        public static DiscordSocketClient RemClient;
        public static string Version = "1.0";
        public static DiscordSocketConfig RemSocketConfig = new DiscordSocketConfig
        {
            DownloadUsersOnGuildAvailable = true,
            LogLevel = LogSeverity.Info,
            MessageCacheSize = 10
        };
        public static CommandService RemService;
        public static Dictionary<string, object> RemConfig;
        public static Dictionary<string, object> RemCredentials;
        public static CommandServiceConfig RemServiceConfig = new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Sync
        };

        public static string GetDescription()
        {
            return (Rem.RemClient.GetApplicationInfoAsync().GetAwaiter().GetResult()).Description;
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Attempting to hand over control to async...");
                Rem.Start().GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("==== CRITICAL ISSUE ====");
                Console.WriteLine("Failed to complete handover.");
                Console.WriteLine($"Cannot connect to Discord! D:");
                Console.WriteLine("Is your internet working?");
                Console.ReadLine();
            }
        }



        public static async Task Start()
        {
            RemClient = new DiscordSocketClient(RemSocketConfig);
            RemService = new CommandService(RemServiceConfig);
            Console.WriteLine("Handover success.");
            Console.WriteLine("Created client, command service and command handler.");
            try
            {
                // Import JSON
                StreamReader RawOpen = File.OpenText(@"./rem_config.json");
                StreamReader RawCredentialsOpen = File.OpenText(@"./credentials.json");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("==== CRITICAL ISSUE ====");
                Console.WriteLine("Failed to access credentials or config.");
                Console.WriteLine($"Cannot read rem_config.json or credentials.json.");
                Console.WriteLine("Follow this guide and try again: https://github.com/iloverem/Rem/blob/master/README.md");
                await Task.Delay(-1);
            }
            StreamReader Raw = File.OpenText(@"./rem_config.json");
            StreamReader RawCredentials = File.OpenText(@"./credentials.json");
            JsonTextReader TextReader = new JsonTextReader(Raw);
            JsonTextReader CredTextReader = new JsonTextReader(RawCredentials);
            JObject MidoriJConfig = (JObject)JToken.ReadFrom(TextReader);
            JObject MidoriCred = (JObject)JToken.ReadFrom(CredTextReader);
            RemConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(MidoriJConfig.ToString());
            RemCredentials = JsonConvert.DeserializeObject<Dictionary<string, object>>(MidoriCred.ToString());

            // Setup dependencies
            IDependencyMap RemDeps = new DependencyMap();
            RemDeps.Add(RemClient);
            RemDeps.Add(RemService);
            RemDeps.Add(MidoriJConfig);
            Console.WriteLine("Organized dependency library.");

            // Events handler
            RemEvents.Setup();
            Console.WriteLine("Installed event handler.");

            // Login and connect
            await RemClient.LoginAsync(TokenType.Bot, (string)RemCredentials["Connection_Token"]);
            Console.WriteLine("Sent login information to Discord.");
            await RemClient.ConnectAsync();
            Console.WriteLine("Connected.");
            await RemClient.DownloadAllUsersAsync();

            // Install command handling
            await RemHandler.Setup(RemDeps);
            Console.WriteLine("Installed commands handler.");

            // Keep the bot running
            await Task.Delay(-1);
        }
    }
}
