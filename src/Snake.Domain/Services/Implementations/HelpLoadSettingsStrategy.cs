using System.Threading.Tasks;
using Snake.Domain.Models;

namespace Snake.Domain.Services.Implementations
{
    public class HelpLoadSettingsStrategy : ILoadSettingsStrategy
    {
        private static bool _isFisrtTimeCallingHelp;

        private readonly IWriterService _writerService;

        public HelpLoadSettingsStrategy(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public LoadSettingsStrategyResult LoadSettings()
        {
            _writerService.WriteLine("Welcome to Snake game help center.");
            _writerService.WriteLine("\t The rules of the game are quite simple:");
            _writerService.WriteLine("\t\t You play with the arrows which move the snake in the direction you want and you try not to hit the walls or yourself!");
            _writerService.WriteLine("\t\t e.g. If you press the left arrow the snake will go to the left side of the game area");
            _writerService.WriteLine("\t\t If you press the up arrow the snake will go to the top side of the game area etc.");
            _writerService.WriteLine("\t If you want to stop for a while just space button (the long one on the keyboard)");
            _writerService.WriteLine("\t If you want to resume the game just hit space button again");
            _writerService.WriteLine("\t If you want to save your game press ctrl + s (I recommend doing so when the game is paused)");
            _writerService.WriteLine("\t If you want to load the saved game type load in the console after this message and follow the instructions you will see!");
            _writerService.WriteLine("\t If you want to set custom speed settings type custom in the console after this message and follow the instructions you will see!");
            if (!_isFisrtTimeCallingHelp)
            {
                _writerService.WriteLine("\t If you want to exit the game just press ctrl + x and you are done but doing so will not save anything!");
            }

            _writerService.WriteLine("That is all the help I can give you. From now on it is up to you how to play the game!");
            _writerService.WriteLine("");

            if (_isFisrtTimeCallingHelp)
            {
                Task.Delay(1_000).GetAwaiter().GetResult();
                _writerService.WriteLine("Oops almost forgot. If you want to exit the game just press ctrl + x and you are done but doing so will not save anything!");
                _isFisrtTimeCallingHelp = false;
            }

            return new LoadSettingsStrategyResult();
        }
    }
}
