using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class midori_QuoteCommand : ModuleBase
    {
        [Command("Quote"), Summary("Gets a quote.")]
        public async Task QuoteCommand([Summary("The seed will affect the choice of quote.")]Int64 Seed = 0)
        {
            Uri uri;
            if (Seed != 0)
            {
                if (Seed.ToString().Length <= 6)
                {
                    uri = new Uri($"http://api.forismatic.com/api/1.0/?method=getQuote&key={Seed}&format=xml&lang=en");
                }
                else
                {
                    await ReplyAsync(":warning: The seed must be less than 6 or empty.");
                    return;
                }
            }
            else
            {
                uri = new Uri($"http://api.forismatic.com/api/1.0/?method=getQuote&format=xml&lang=en");
            }

            HttpWebRequest objRegistration = (HttpWebRequest)WebRequest.Create(uri);

            var response = await objRegistration.GetResponseAsync();
            XmlDocument XMLResponse = new XmlDocument();
            XMLResponse.Load(response.GetResponseStream());
            await ReplyAsync(XMLResponse.GetElementsByTagName("quoteText")[0].InnerText + $" - _{XMLResponse.GetElementsByTagName("quoteAuthor")[0].InnerText}_");
        }
    }
}
