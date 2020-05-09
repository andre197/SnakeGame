namespace Snake.Game
{
    using System;
    using Snake.Domain.Services;

    public class ConsoleReader : IReaderService
    {
        public string Read()
            => Console.ReadLine();

    }
}
