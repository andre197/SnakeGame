namespace Snake.Console.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Snake.Domain.Models;
    using Snake.Domain.Services;

    public class FileSettingsLoaderService : ISettingsLoader
    {
        private List<string> _loadedSettings;

        public IEnumerable<string> FindSettings()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var settingsDir = Path.Combine(baseDir, PathConstants.SettingsDir);
            if (!Directory.Exists(settingsDir))
            {
                Directory.CreateDirectory(settingsDir);
                return null;
            }

            _loadedSettings = Directory.GetFiles(settingsDir).Where(f => f.EndsWith(PathConstants.SettingsFilePostfix)).ToList();
            return _loadedSettings;
            throw new NotImplementedException();
        }

        public GameSettings Load(int idx)
        {
            if (!File.Exists(_loadedSettings[idx]))
            {
                return new GameSettings();
            }

            return JsonConvert.DeserializeObject<GameSettings>(File.ReadAllText(_loadedSettings[idx]));
        }
    }
}
