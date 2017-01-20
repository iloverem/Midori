using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using MidoriBot.Common.Preconditions;
using MidoriBot.Common;
using System.Text;

namespace MidoriBot.Modules
{
    [Name("Help"), Group]
    public sealed class midori_HelpCommand : ModuleBase
    {
        private readonly CommandService MidoriCommands;
        private readonly IDependencyMap MidoriDeps;

        public midori_HelpCommand(CommandService _MidoriCommands, IDependencyMap _MidoriDeps)
        {
            if (_MidoriCommands == null) throw new ArgumentNullException(nameof(_MidoriCommands));

            MidoriCommands = _MidoriCommands;
            MidoriDeps = _MidoriDeps;
        }

        [Command("Help"), Summary("View all my commands!"), Hidden, MinPermissions(AccessLevel.User)]
        public async Task CommandHelp()
        {
            IEnumerable<IGrouping<string, CommandInfo>> CommandGroups = (await MidoriCommands.Commands.CheckConditions(Context, MidoriDeps))
                .Where(c => !c.Preconditions.Any(p => p is HiddenAttribute))
                .GroupBy(c => (c.Module.IsSubmodule ? c.Module.Parent.Name : c.Module.Name));

            NormalEmbed HelpEmbed = new NormalEmbed();
            StringBuilder HEDesc = new StringBuilder();

            HelpEmbed.Title = "My Commands";
            HelpEmbed.ThumbnailUrl = Context.Client.CurrentUser.AvatarUrl;
            HEDesc.AppendLine("**You can use the following commands:**");

            foreach (IGrouping<string, CommandInfo> Group in CommandGroups)
            {
                if (Group.Key == "Reactions" || Group.Key == "Emojis")
                {
                    StringBuilder sb = new StringBuilder();
                    List<string> reactions = new List<string> { };
                    foreach (CommandInfo Command in Group)
                    {
                        reactions.Add("`" + Command.Name + "` ");
                        sb.Append($"`{Command.Name}` ");

                    }
                    HEDesc.AppendLine($"**{Group.Key}**: " + sb.ToString());
                }
                else
                {
                    HEDesc.AppendLine($"**{Group.Key}**:");
                    foreach (CommandInfo Command in Group)
                    {
                        HEDesc.AppendLine($"• `{Command.Name}`: {Command.Summary}");
                    }
                }
            }
            HEDesc.AppendLine($"\nYou can use `{Midori.MidoriConfig["Command_Prefix"]}Help <commandorgroup>` for more information on that command/group.");

            HelpEmbed.Description = HEDesc.ToString();
            await (Context.User.CreateDMChannelAsync().Result).SendEmbedAsync(HelpEmbed);
            if (!Context.IsPrivate)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}, help sent to your Direct Messages!");
            }
        }

        [Command("Help"), Summary("Shows summary for a command or group."), Hidden]
        public async Task SpecificHelp([Remainder] string cmdname)
        {
            IEnumerable<IGrouping<string, CommandInfo>> CommandGroups = (await MidoriCommands.Commands.CheckConditions(Context, MidoriDeps))
                .Where(c => !c.Preconditions.Any(p => p is HiddenAttribute))
                .GroupBy(c => (c.Module.IsSubmodule ? c.Module.Parent.Name : c.Module.Name));

            IGrouping<string, CommandInfo> Target = CommandGroups.FirstOrDefault(x => x.Key.ToUpper() == cmdname.ToUpper());
            if (Target != null)
            {
                StringBuilder HEDesc = new StringBuilder();
                HEDesc.AppendLine($"**{Target.Key}**:");
                foreach (CommandInfo CommandDetails in Target)
                {
                    HEDesc.AppendLine($"• `{CommandDetails.Name}`: {CommandDetails.Summary}");
                }
                HEDesc.AppendLine($"\nYou can use `{Midori.MidoriConfig["Command_Prefix"]}Help <commandorgroup>` for more information on that command/group.");
                NormalEmbed ModuleHelp = new NormalEmbed();
                ModuleHelp.Title = $"Group {Target.Key}";
                ModuleHelp.Description = HEDesc.ToString();
                await Context.User.CreateDMChannelAsync().GetAwaiter().GetResult().SendEmbedAsync(ModuleHelp);
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}, help for module `{Target.Key}` sent to your Direct Messages!");
                return;
            }

            StringBuilder sb = new StringBuilder();
            NormalEmbed e = new NormalEmbed();
            IEnumerable<CommandInfo> Commands = (await MidoriCommands.Commands.CheckConditions(Context, MidoriDeps))
                .Where(c => (c.Aliases.FirstOrDefault().Equals(cmdname, StringComparison.OrdinalIgnoreCase) ||
                (c.Module.IsSubmodule ? c.Module.Aliases.FirstOrDefault().Equals(cmdname, StringComparison.OrdinalIgnoreCase) : false))
                    && !c.Preconditions.Any(p => p is HiddenAttribute));

            if (Commands.Any())
            {
                await ReplyAsync($"{Commands.Count()} {(Commands.Count() > 1 ? $"entries" : "entry")} for `{cmdname}`");

                foreach (CommandInfo Command in Commands)
                {
                    NormalEmbed x = new NormalEmbed();
                    x.Title = $"{Command.Name}";
                    x.Description = (Command.Summary ?? "No summary.");
                    x.AddField(a =>
                    {
                        a.Name = "Usage";
                        a.IsInline = true;
                        a.Value = $"{Midori.MidoriConfig["Command_Prefix"]}{(Command.Module.IsSubmodule ? $"{Command.Module.Name} " : "")}{Command.Name} " + string.Join(" ", Command.Parameters.Select(p => formatParam(p))).Replace("`", "") + " ";
                    });
                    x.AddField(a =>
                    {
                        a.Name = "Aliases";
                        a.IsInline = true;
                        StringBuilder s = new StringBuilder();
                        if (Command.Aliases.Any())
                        {
                            foreach (string Alias in Command.Aliases)
                            {
                                s.Append(Alias + " ");
                            }
                        }
                        a.Value = $"{(Command.Aliases.Any() ? s.ToString() : "No aliases.")}";
                    });
                    await Context.Channel.SendEmbedAsync(x);
                }
            }
            else
            {
                await ReplyAsync($":warning: I couldn't find any command or group matching `{cmdname}`.");
                return;
            }
        }

        private string formatParam(ParameterInfo param)
        {
            var sb = new StringBuilder();
            if (param.IsMultiple)
            {
                sb.Append($"`[({param.Type.Name}): {param.Name}...]`");
            }
            else if (param.IsRemainder)
            {
                sb.Append($"`<({param.Type.Name}): {param.Name}...>`");
            }
            else if (param.IsOptional)
            {
                sb.Append($"`[({param.Type.Name}): {param.Name}]`");
            }
            else
            {
                sb.Append($"`<({param.Type.Name}): {param.Name}>`");
            }

            if (!string.IsNullOrWhiteSpace(param.Summary))
            {
                sb.Append($" ({param.Summary})");
            }
            return sb.ToString();
        }
    }
}
