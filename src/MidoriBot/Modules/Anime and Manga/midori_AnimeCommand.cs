﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using System.Xml;
using MidoriBot.Common;

namespace MidoriBot.Modules.Anime_and_Manga
{
    [Name("Anime and Manga")]
    public class midori_AnimeCommand : ModuleBase
    {
        [Command("Anime"), Summary("Gets anime information from MyAnimeList.")]
        public async Task AnimeCommand([Remainder, Summary("Anime to search for.")] string AnimeTarget)
        {
            /*var request = WebRequest.Create("https://myanimelist.net/api/anime/search.xml?q=full+metal");
            SetBasicAuthHeader(request, "midoribot", "$MidoriBot212121212121212121");
            var response = await request.GetRequestStreamAsync();
            /*XmlDocument XMLResponse = new XmlDocument();
            XMLResponse.Load(response);
            XmlNodeList AnimeTitle = XMLResponse.GetElementsByTagName("title");
            await ReplyAsync(AnimeTitle[0].InnerText);
            await ReplyAsync("hello");
            */
            string ModifiedAnimeTarget = AnimeTarget.Replace(' ', '+');
            Uri uri = new Uri($"https://myanimelist.net/api/anime/search.xml?q={ModifiedAnimeTarget}");

            HttpWebRequest objRegistration = (HttpWebRequest)WebRequest.Create(uri);

            CredentialCache credentials = new CredentialCache();

            NetworkCredential netCredential = new NetworkCredential((string)Midori.MidoriConfig["MalUsername"], (string)Midori.MidoriConfig["MalPassword"]);

            credentials.Add(uri, "Basic", netCredential);

            objRegistration.Credentials = credentials;

            var response = await objRegistration.GetResponseAsync();
            XmlDocument XMLResponse = new XmlDocument();
            XMLResponse.Load(response.GetResponseStream());
            NormalEmbed AnimeEmbed = new NormalEmbed();
            AnimeEmbed.Title = $"{GetInnerText(XMLResponse.GetElementsByTagName("title"))}{(GetInnerText(XMLResponse.GetElementsByTagName("english")) != null ? " (" + GetInnerText(XMLResponse.GetElementsByTagName("title")) + ")" : "")}";
            AnimeEmbed.Url = $"http://myanimelist.net/anime/{GetInnerText(XMLResponse.GetElementsByTagName("id"))}";
            AnimeEmbed.Description = FormatDescription(GetInnerText(XMLResponse.GetElementsByTagName("synopsis")));
            AnimeEmbed.ImageUrl = GetInnerText(XMLResponse.GetElementsByTagName("image"));
            AnimeEmbed.AddField(x =>
            {
                x.Name = "Episodes";
                x.Value = GetInnerText(XMLResponse.GetElementsByTagName("episodes"));
                x.IsInline = true;
            });
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
                a.Value = $"{GetInnerText(XMLResponse.GetElementsByTagName("start_date"))} / {GetInnerText(XMLResponse.GetElementsByTagName("end_date"))}";
                a.IsInline = true;
            });
            await Context.Channel.SendEmbedAsync(AnimeEmbed);
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
