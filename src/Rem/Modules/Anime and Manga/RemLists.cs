using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace Rem.Modules.Anime_and_Manga
{
    [Name("Anime and Manga")]
    public class RemLists : ModuleBase
    {
        [Command("MAL"), Summary("Gets your MyAnimeList library.")]
        public async Task MALList([Summary("Your MAL username")] string username)
        {
            await ReplyAsync($"Here's your MyAnimeList animelist: http://myanimelist.net/animelist/{username}");
        }
    }
}
