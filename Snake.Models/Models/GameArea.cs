namespace Snake.Domain.Models
{
    using System.Drawing;
    using Domain.Interfaces;
    using Domain.Services;

    public class GameArea : IDrawable
    {
        public GameArea(Point upperLeftCoordinates, Point bottomRightCoordinates)
        {
            UpperLeftCoordinates = upperLeftCoordinates;
            BottomRightCoordinates = bottomRightCoordinates;
        }

        public Point UpperLeftCoordinates { get; }

        public Point BottomRightCoordinates { get; }

        public bool IsPointOnEdgeOfGameArea(Point p)
        => !(p.X > UpperLeftCoordinates.X 
             && p.X < BottomRightCoordinates.X - 1
             && p.Y > UpperLeftCoordinates.Y 
             && p.Y < BottomRightCoordinates.Y - 1);

        public void Draw(IWriterService writerService)
        {
            writerService.Clear();
            var border = new string('#', BottomRightCoordinates.X - UpperLeftCoordinates.X);
            writerService.SetWritePosition(UpperLeftCoordinates.Y, UpperLeftCoordinates.X);
            writerService.WriteLine(border);
            for (int i = UpperLeftCoordinates.Y + 1; i < BottomRightCoordinates.Y - 1; i++)
            {
                writerService.SetWritePosition(i, UpperLeftCoordinates.X);
                writerService.Write("#");
                writerService.SetWritePosition(i, BottomRightCoordinates.X - 1);
                writerService.WriteLine("#");
            }

            writerService.WriteLine(border);
        }
    }
}
