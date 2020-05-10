namespace Snake.Console.App
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Snake.Console.Infrastructure;
    using Snake.Domain;
    using Snake.Domain.Services;
    using Snake.Domain.Services.Implementations;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.BufferWidth = 200;

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IReaderService, ConsoleReader>()
                .AddSingleton<IWriterService, ConsoleWriter>()
                .AddSingleton<ILowLevelEventsService, LowLevelKeyboardHook>()
                .AddSingleton<ISettingsLoader, FileSettingsLoaderService>()
                .AddSingleton<ISettingsSaver, FileSettingsSaverService>()
                .AddSingleton<IHighLevelEventsService, HighLevelEventsService>()
                .AddSingleton<INewPointFactory, NewPointFactory>()
                .AddSingleton<ILoadSettingsStrategyFactory, LoadSettingsStrategyFactory>()
                .AddSingleton<Game, Game>();

            Console.Title = "Snake";

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var game = serviceProvider.GetService<Game>();
            try
            {
                game.Start();
            }
            finally
            {
                game.Dispose();
            }
        }
    }
}
