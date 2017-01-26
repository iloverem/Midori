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
            Console.WriteLine("Handover success.");
            Console.Write("Attempting to initialize client and service... ");
            RemClient = new DiscordSocketClient(RemSocketConfig);
            RemService = new CommandService(RemServiceConfig);
            Console.Write("Done.");
            Console.WriteLine();
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
            Console.Write("Attempting to deserialize configuration and credential files.... ");
            StreamReader Raw = File.OpenText(@"./rem_config.json");
            StreamReader RawCredentials = File.OpenText(@"./credentials.json");
            JsonTextReader TextReader = new JsonTextReader(Raw);
            JsonTextReader CredTextReader = new JsonTextReader(RawCredentials);
            JObject MidoriJConfig = (JObject)JToken.ReadFrom(TextReader);
            JObject MidoriCred = (JObject)JToken.ReadFrom(CredTextReader);
            RemConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(MidoriJConfig.ToString());
            RemCredentials = JsonConvert.DeserializeObject<Dictionary<string, object>>(MidoriCred.ToString());
            Console.Write("Done.");
            Console.WriteLine();

            // Setup dependencies
            Console.Write("Creating dependency library... ");
            IDependencyMap RemDeps = new DependencyMap();
            RemDeps.Add(RemClient);
            RemDeps.Add(RemService);
            RemDeps.Add(MidoriJConfig);
            Console.Write("Done.");
            Console.WriteLine();

            // Events handler
            Console.Write("Installing events handler.... ");
            RemEvents.Setup();
            Console.Write("Done.");
            Console.WriteLine();

            // Login and connect
            Console.Write("Logging in... ");
            await RemClient.LoginAsync(TokenType.Bot, (string)RemCredentials["Connection_Token"]);
            Console.Write("Done.");
            Console.WriteLine();
            Console.Write("Connecting.... ");
            await RemClient.ConnectAsync();
            Console.Write("Done.");
            Console.WriteLine();
            Console.Write("Downloading users.... ");
            await RemClient.DownloadAllUsersAsync();
            Console.Write("Done.");
            Console.WriteLine();

            // Install command handling
            Console.Write("Installing commands handler... ");
            await RemHandler.Setup(RemDeps);
            Console.Write("Done.");
            Console.WriteLine();

            // Ready
            Console.WriteLine("=====");
            Console.WriteLine((Rem.RemClient.GetApplicationInfoAsync().GetAwaiter().GetResult()).Description);
            Console.WriteLine("Active token: " + Rem.RemCredentials["Connection_Token"]);
            Console.WriteLine("Active command prefix: " + Rem.RemConfig["Command_Prefix"]);
            Console.WriteLine("Accepting bot commands: " + ((bool)Rem.RemConfig["AcceptBotCommands"] ? "Yes." : "No."));
            Console.WriteLine("Alerting on unknown command: " + ((bool)Rem.RemConfig["AlertOnUnknownCommands"] ? "Yes." : "No."));
            Console.WriteLine("=====");

            // Keep the bot running
            await Task.Delay(-1);
        }
    }
}
