namespace Snake.Domain.Services.Implementations
{
    using Snake.Domain.Models;

    internal class CustomLoadSettingsStrategy : ILoadSettingsStrategy
    {
        private IWriterService _writerService;
        private IReaderService _readerService;

        public CustomLoadSettingsStrategy(IWriterService writerService, IReaderService readerService)
        {
            _writerService = writerService;
            _readerService = readerService;
        }

        public LoadSettingsStrategyResult LoadSettings()
        {
            while (true)
            {
                _writerService.WriteLine("To change the default speed please enter number between 1 and 16 or press enter. The default speed is 1:");
                var inp = _readerService.Read();
                if (!int.TryParse(inp, out int speed) || speed < 1 || speed > 16)
                {
                    _writerService.WriteLine("Invalid input! Please try again!");
                    continue;
                }

                return new LoadSettingsStrategyResult
                {
                    GameSettings = new GameSettings
                    {
                        Speed = speed
                    },
                    Loaded = true
                };
            }

        }
    }
}