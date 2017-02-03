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
            bool SetLeft = true;
            int Loops = 0;
            IUserMessage Message = await Context.Channel.SendMessageAsync(@"\o\");
            while (Loops < 10)
            {
                if (SetLeft)
                {
                    await Message.ModifyAsync(x =>
                    {
                        x.Content = @"\o\";
                    });
                    SetLeft = false;
                }
                else
                {
                    await Message.ModifyAsync(x =>
                    {
                        x.Content = @"/o/";
                    });
                    SetLeft = true;
                }
                Loops++;
            }
        }
    }
}
