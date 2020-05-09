namespace Snake.Domain.Services
{
    using System.Drawing;
    using Snake.Domain.Enums;

    public interface INewPointFactory
    {
        Point GetNewPoint(MoveKey moveKey, Point oldPoint);
    }
}
