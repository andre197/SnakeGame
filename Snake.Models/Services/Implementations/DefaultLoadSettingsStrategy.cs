namespace Snake.Domain.Services.Implementations
{
    using Snake.Domain.Models;

    public class DefaultLoadSettingsStrategy : ILoadSettingsStrategy
    {
        private readonly IWriterService _writerService;

        public DefaultLoadSettingsStrategy(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public LoadSettingsStrategyResult LoadSettings()
        {
            _writerService.WriteLine("Invalid input! To start the game please press enter or type help to get help with the game!");
            return new LoadSettingsStrategyResult();
        }
    }
}
