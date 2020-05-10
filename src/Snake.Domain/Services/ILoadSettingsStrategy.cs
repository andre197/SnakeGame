namespace Snake.Domain.Services
{
    using Snake.Domain.Models;

    public interface ILoadSettingsStrategy
    {
        LoadSettingsStrategyResult LoadSettings();
    }
}
