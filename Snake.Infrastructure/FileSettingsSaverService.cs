namespace Snake.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Snake.Domain.Services;
    using Snake.Domain.Models;
    using System.Linq;
    using Newtonsoft.Json;
    using System.IO;

    public class FileSettingsSaverService : ISettingsSaver
    {
        private readonly ISettingsLoader _settingsLoader;

        public FileSettingsSaverService(ISettingsLoader settingsLoader)
        {
            _settingsLoader = settingsLoader;
        }

        public void Save(GameSettings gameSettings)
        {
            var settings = _settingsLoader.FindSettings();
            var filePrefixNumber = "File" + ((settings?.Count() ?? 0) + 1);

            var fileName = filePrefixNumber + PathConstants.SettingsFilePostfix;
            var settingsJson = JsonConvert.SerializeObject(gameSettings);

            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PathConstants.SettingsDir);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fullFileName = Path.Combine(directory, fileName);
            File.WriteAllText(fullFileName, settingsJson);
        }
    }
}
