namespace Snake.Domain.Services.Implementations
{
    using System;
    using System.Drawing;
    using Snake.Domain.Enums;

    public class NewPointFactory : INewPointFactory
    {
        public Point GetNewPoint(MoveKey e, Point oldPoint)
        {
            switch (e)
            {
                case MoveKey.Up:
                    return new Point(oldPoint.X, oldPoint.Y - 1);
                case MoveKey.Down:
                    return new Point(oldPoint.X, oldPoint.Y + 1);
                case MoveKey.Left:
                    return new Point(oldPoint.X - 1, oldPoint.Y);
                case MoveKey.Right:
                    return new Point(oldPoint.X + 1, oldPoint.Y);
                default:
                    throw new ArgumentException("Invalid move key value: " + e);
            }
        }
    }
}
