namespace Snake.Domain.Interfaces
{
    using Snake.Domain.Services;

    public interface IDrawable
    {
        void Draw(IWriterService writerService);
    }
}