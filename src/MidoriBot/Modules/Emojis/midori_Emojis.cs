using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace MidoriBot.Modules.Emojis
{
    [Name("Emojis")]
    public class midori_Emojis : ModuleBase
    {
        [Command("Lenny"), Summary("( ͡° ͜ʖ ͡°)")]
        public async Task LennyCommand() => await ReplyAsync("( ͡° ͜ʖ ͡°)");
        [Command("Teddy"), Summary("ʕ•ᴥ•ʔ")]
        public async Task TeddyCommand() => await ReplyAsync("ʕ•ᴥ•ʔ");
        [Command("Fite"), Summary("(ง ͠° ͟ل͜ ͡°)ง")]
        public async Task FiteCommand() => await ReplyAsync("(ง ͠° ͟ل͜ ͡°)ง");
        [Command("Wot"), Summary("ಠ_ಠ")]
        public async Task WotCommand() => await ReplyAsync("ಠ_ಠ");
    }
}
