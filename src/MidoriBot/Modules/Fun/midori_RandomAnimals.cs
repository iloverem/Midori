using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MidoriBot.Common;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class midori_RandomAnimals : ModuleBase
    {
        [Command("Cat"), Summary("Gets a random cat!")]
        public async Task CatCommand()
        {
            Uri CatUri = new Uri("http://random.cat/meow");
            HttpWebRequest CatRequest = (HttpWebRequest)WebRequest.Create(CatUri);
            var Response = CatRequest.GetResponseAsync();
            var ResponseStream = (await Response).GetResponseStream();
            StreamReader CatStream = new StreamReader(ResponseStream);
            JsonTextReader Reader = new JsonTextReader(CatStream);
            JObject CatJ = (JObject)JToken.ReadFrom(Reader);
            Dictionary<string, string> Result = JsonConvert.DeserializeObject<Dictionary<string, string>>(CatJ.ToString());
            NormalEmbed Cat = new NormalEmbed();
            Cat.Title = "Cat.";
            Cat.ImageUrl = Result["file"];
            Cat.Footer = new MEmbedFooter(Context.Client).WithText("Courtesy of http://random.cat");
            await Context.Channel.SendEmbedAsync(Cat);
        }
    }
}
