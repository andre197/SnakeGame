namespace Snake.Console.App
{
    using System;
    using System.Linq;
    using Snake.Domain.Services;

    public class ConsoleWriter : IWriterService
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void OverWriteAll(string message)
        {
            var messageLines = message.Split(Environment.NewLine);
            var messageWidth = messageLines.Select(ml => ml.Length).Max();
            SetWritePosition(0, 0);
            WriteLine(message);
            SetWritePosition(messageLines.Length, 0);
        }

        public void SetWritePosition(int row, int col)
        {
            Console.SetCursorPosition(col, row);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
