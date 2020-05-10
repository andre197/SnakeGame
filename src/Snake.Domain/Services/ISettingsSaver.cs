namespace Snake.Domain.Services
{
    using Snake.Domain.Models;

    public interface ISettingsSaver
    {
        void Save(GameSettings gameSettings);
    }
}
