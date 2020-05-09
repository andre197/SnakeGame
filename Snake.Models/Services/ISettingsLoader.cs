namespace Snake.Domain.Services
{
    using System.Collections.Generic;
    using Snake.Domain.Models;

    public interface ISettingsLoader
    {
        IEnumerable<string> FindSettings();

        GameSettings Load(int idx);
    }
}
