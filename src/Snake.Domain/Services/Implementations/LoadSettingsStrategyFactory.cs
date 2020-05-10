namespace Snake.Domain.Services.Implementations
{
    public class LoadSettingsStrategyFactory : ILoadSettingsStrategyFactory
    {
        private IReaderService _readerService;
        private IWriterService _writerService;
        private readonly ISettingsLoader _settingsLoader;

        public LoadSettingsStrategyFactory(IReaderService readerService, IWriterService writerService, ISettingsLoader settingsLoader)
        {
            _readerService = readerService;
            _writerService = writerService;
            _settingsLoader = settingsLoader;
        }

        public ILoadSettingsStrategy GetStrategy(string input)
        {
            switch (input.ToLower())
            {
                case "help":
                    return new HelpLoadSettingsStrategy(_writerService);
                case null:
                case "":
                case " ":
                    return new DefaultSettingsLoadSettingsFactory();
                case "load":
                    return new LoadLoadSettingsStrategy(_settingsLoader, _writerService, _readerService);
                case "custom":
                    return new CustomLoadSettingsStrategy(_writerService, _readerService);
                default:
                    return new DefaultLoadSettingsStrategy(_writerService);
            }
        }
    }
}
