namespace Snake.Domain.Services.Implementations
{
    using System.Linq;
    using Snake.Domain.Models;

    public class LoadLoadSettingsStrategy : ILoadSettingsStrategy
    {
        private readonly ISettingsLoader _settingsLoader;
        private readonly IWriterService _writerService;
        private readonly IReaderService _readerService;

        public LoadLoadSettingsStrategy(ISettingsLoader settingsLoader, IWriterService writerService, IReaderService readerService)
        {
            _settingsLoader = settingsLoader;
            _writerService = writerService;
            _readerService = readerService;
        }
        public LoadSettingsStrategyResult LoadSettings()
        {
            var savedSettings = _settingsLoader.FindSettings();
            var savedSettingsCount = savedSettings.Count();
            if (savedSettings == null || savedSettingsCount == 0)
            {
                _writerService.WriteLine("Saved settings cannot be found! The game will start with the default settings!");
                return new LoadSettingsStrategyResult();
            }

            int index = 1;
            _writerService.WriteLine("Index | Settings name");
            foreach (var item in savedSettings)
            {
                _writerService.WriteLine($"{index:D5} | {item}");
                index++;

            }

            bool isFirstRun = true;
            while (true)
            {
                _writerService.WriteLine("Type the index of file with saved settings from the files written above and press enter!");
                if (!isFirstRun)
                {
                    _writerService.WriteLine("If you want to start with default settings press enter!");
                }

                var input = _readerService.Read();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return new LoadSettingsStrategyResult();
                }


                if (int.TryParse(input, out int idx) && 0 < idx && idx <= savedSettingsCount)
                {
                    return new LoadSettingsStrategyResult { Loaded = true, GameSettings = _settingsLoader.Load(idx - 1) };
                }

                _writerService.WriteLine("Invalid input please try again!");

                isFirstRun = false;
            }
        }
    }
}
