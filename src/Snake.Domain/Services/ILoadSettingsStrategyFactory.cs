namespace Snake.Domain.Services
{
    public interface ILoadSettingsStrategyFactory
    {
        ILoadSettingsStrategy GetStrategy(string input);
    }
}
