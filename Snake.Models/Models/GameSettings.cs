namespace Snake.Domain.Models
{
    using System.Collections.Generic;
    using System.Drawing;

    public class GameSettings
    {
        private int minSpeedInMs = 500;

        public int Speed { get; set; } = 1;

        public Size GameAreaSize { get; set; } = new Size(150, 30);

        public List<Point> Snake { get; set; } = new List<Point>
        {
            new Point(50, 15),
            new Point(51, 15)
        };

        public int GetSpeedInMs()
            => minSpeedInMs / Speed;
    }
}
