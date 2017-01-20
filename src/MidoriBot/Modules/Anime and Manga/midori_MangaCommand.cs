using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using System.Net;
using MidoriBot.Common;
using System.Xml;

namespace MidoriBot.Modules.Anime_and_Manga
{
	[Name("Anime and Manga")]
    public class midori_MangaCommand : ModuleBase
    {
		[Command("Manga"), Summary("Gets manga information from MyAnimeList.")]
        public async Task MangaCommand([Remainder, Summary("Manga to search for.")] string MangaTarget)
        {
            string ModifiedMangaTarget = MangaTarget.Replace(' ', '+');
            Uri uri = new Uri($"https://myanimelist.net/api/manga/search.xml?q={ModifiedMangaTarget}");

            HttpWebRequest objRegistration = (HttpWebRequest)WebRequest.Create(uri);

            CredentialCache credentials = new CredentialCache();

            NetworkCredential netCredential = new NetworkCredential((string)Midori.MidoriCredentials["MalUsername"], (string)Midori.MidoriCredentials["MalPassword"]);

            credentials.Add(uri, "Basic", netCredential);

            objRegistration.Credentials = credentials;

            var response = await objRegistration.GetResponseAsync();
            XmlDocument XMLResponse = new XmlDocument();
            try
            {
                XMLResponse.Load(response.GetResponseStream());
            }
            catch
            {
                await ReplyAsync(":warning: I couldn't find that manga.");
                return;
            }
            /*NormalEmbed AnimeEmbed = new NormalEmbed();
            AnimeEmbed.Title = $"{GetInnerText(XMLResponse.GetElementsByTagName("title"))}{(GetInnerText(XMLResponse.GetElementsByTagName("english")) != null ? " (" + GetInnerText(XMLResponse.GetElementsByTagName("title")) + ")" : "")}";
            AnimeEmbed.Url = $"http://myanimelist.net/anime/{GetInnerText(XMLResponse.GetElementsByTagName("id"))}";
            AnimeEmbed.Description = FormatDescription(GetInnerText(XMLResponse.GetElementsByTagName("synopsis")));
            AnimeEmbed.ThumbnailUrl = GetInnerText(XMLResponse.GetElementsByTagName("image"));
            if (GetInnerText(XMLResponse.GetElementsByTagName("episodes")) != "0")
            {
                AnimeEmbed.AddField(x =>
                {
                    x.Name = "Episodes";
                    x.Value = GetInnerText(XMLResponse.GetElementsByTagName("episodes"));
                    x.IsInline = true;
                });
            }
            AnimeEmbed.AddField(a =>
            {
                a.Name = "Type";
                a.Value = GetInnerText(XMLResponse.GetElementsByTagName("type"));
                a.IsInline = true;
            });
            AnimeEmbed.AddField(a =>
            {
                a.Name = "Status";
                a.Value = GetInnerText(XMLResponse.GetElementsByTagName("status"));
                a.IsInline = true;
            });
            AnimeEmbed.AddField(a =>
            {
                a.Name = "Start Date / End Date";
                a.Value = $"{GetInnerText(XMLResponse.GetElementsByTagName("start_date"))}{(GetInnerText(XMLResponse.GetElementsByTagName("end_date")) != "0000-00-00" ? " / " + GetInnerText(XMLResponse.GetElementsByTagName("end_date")) : "")}";
                a.IsInline = true;
            });
            AnimeEmbed.Footer = (new MEmbedFooter(Context.Client)).WithText($"All information from the MyAnimeList API").WithIconUrl("http://i.imgur.com/vEy5Zaq.png");
            await Context.Channel.SendEmbedAsync(AnimeEmbed);
			*/
            NormalEmbed Manga = new NormalEmbed();
            Manga.Title = $"{GetInnerText(XMLResponse.GetElementsByTagName("title"))} {((GetInnerText(XMLResponse.GetElementsByTagName("english"))) != null ? $" {GetInnerText(XMLResponse.GetElementsByTagName("english"))}" : "")}";
            Manga.Description = FormatDescription(GetInnerText(XMLResponse.GetElementsByTagName("synopsis")));
            Manga.ThumbnailUrl = GetInnerText(XMLResponse.GetElementsByTagName("image"));
            if (GetInnerText(XMLResponse.GetElementsByTagName("chapters")) != "0")
            {
                Manga.AddField(f =>
                {
                    f.Name = "Chapters";
                    f.Value = GetInnerText(XMLResponse.GetElementsByTagName("chapters"));
                    f.IsInline = true;
                });
            }
            if (GetInnerText(XMLResponse.GetElementsByTagName("volumes")) != "0")
            {
                Manga.AddField(f =>
                {
                    f.Name = "Volumes";
                    f.Value = GetInnerText(XMLResponse.GetElementsByTagName("volumes"));
                    f.IsInline = true;
                });
            }
            Manga.AddField(f =>
            {
                f.Name = "Score";
                f.Value = GetInnerText(XMLResponse.GetElementsByTagName("score"));
                f.IsInline = true;
            });
            Manga.AddField(f =>
            {
                f.Name = "Type";
                f.Value = GetInnerText(XMLResponse.GetElementsByTagName("type"));
                f.IsInline = true;
            });
            Manga.AddField(f =>
            {
                f.Name = "Status";
                f.Value = GetInnerText(XMLResponse.GetElementsByTagName("status"));
                f.IsInline = true;
            });
            Manga.AddField(f =>
            {
                f.Name = "Start Date / End Date";
                f.Value = $"{GetInnerText(XMLResponse.GetElementsByTagName("start_date"))} {((GetInnerText(XMLResponse.GetElementsByTagName("end_date"))) != "0000-00-00" ? " / " + GetInnerText(XMLResponse.GetElementsByTagName("end_date")) : "")}";
                f.IsInline = true;
            });
            await Context.Channel.SendEmbedAsync(Manga);
        }
        private string GetInnerText(XmlNodeList NodeList)
        {
            return NodeList[0].InnerText;
        }
        private string FormatDescription(string Description)
        {
            string[] seperators = new string[] { "<br />" };
            string[] result;
            result = Description.Split(seperators, StringSplitOptions.None);
            return FormatDescriptionComplete(result[0]);
        }
        private string FormatDescriptionComplete(string Input)
        {
            Dictionary<string, string> HTMLEntities = new Dictionary<string, string>()
            {
                {"&rsquo;", "'" },
                {"&quot;", "\"" },
                {"<br />", "\n" },
                {"[i]", "_" },
                {"[/i]", "_" },
                {"&#039;", "'" },
                {"&mdash;", "-" }
            };
            string x = Input;
            foreach (KeyValuePair<string, string> Pair in HTMLEntities)
            {
                x = x.Replace(Pair.Key, Pair.Value);
            }
            return x;
        }
    }
}
