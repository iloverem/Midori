using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace Rem.Modules.Fun
{
    [Name("Fun")]
    public class RemRussianRoulette : ModuleBase
    {
        [Command("RR"), Alias("RussianRoulette"), Summary("Plays Russian Roulette.")]
        public async Task RussianRoulette()
        {
            Random RandomClient = new Random();
            await ReplyAsync($"{(RandomClient.Next(1, 6) == 1 ? ":skull_crossbones: You have died!" : ":information_source: You are alive. You should stop playing.")}");
        }
    }
}
