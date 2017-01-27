using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace MidoriBot.Modules.Fun
{
    [Name("Fun")]
    public class RemDanceCommand : ModuleBase
    {
        [Command("Dance"), Summary("Dances!")]
        public async Task DanceCommand()
        {
            bool Left = true;
            int Loops = 0;
            IUserMessage Message = await Context.Channel.SendMessageAsync(@"\o\");
            while (Loops < 10)
            {
                Left = !Left;
                if (Left)
                {
                    await Message.ModifyAsync(x =>
                    {
                        x.Content = @"\o\";
                    });
                }
                else
                {
                    await Message.ModifyAsync(x =>
                    {
                        x.Content = @"/o/";
                    });
                }
                Loops++;
            }
        }
    }
}
