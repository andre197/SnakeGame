namespace Snake.Domain.Services.Implementations
{
    using Snake.Domain.Models;

    public class DefaultSettingsLoadSettingsFactory : ILoadSettingsStrategy
    {
        public LoadSettingsStrategyResult LoadSettings()
            => new LoadSettingsStrategyResult
            {
                GameSettings = new GameSettings(),
                Loaded = true
            };
    }
}
