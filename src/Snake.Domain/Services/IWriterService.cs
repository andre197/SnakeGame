namespace Snake.Domain.Services
{
    public interface IWriterService
    {
        void Clear();

        void SetWritePosition(int row, int col);

        void Write(string message);

        void WriteLine(string message);

        void OverWriteAll(string message);
    }
}
