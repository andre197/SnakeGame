namespace Snake.Domain.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Domain.Interfaces;
    using global::Snake.Domain.Services;

    public class Snake : IDrawable
    {
        private bool _isFirstDraw = true;
        private readonly List<Point> _activePoints;

        private Point? _lastRemovedPoint;

        public Snake(IEnumerable<Point> body, Point head)
        {
            _activePoints = new List<Point>(body);
            HeadCoordinates = head;
        }

        public Point HeadCoordinates { get; private set; }

        public IEnumerable<Point> ActivePoints => _activePoints;

        public bool HasBittenHerself => _activePoints.Contains(HeadCoordinates);

        public void Draw(IWriterService writerService)
        {
            if (_lastRemovedPoint.HasValue)
            {
                writerService.SetWritePosition(_lastRemovedPoint.Value.Y, _lastRemovedPoint.Value.X);
                writerService.WriteLine(" ");
            }

            if (_isFirstDraw)
            {
                foreach (var p in _activePoints)
                {
                    writerService.SetWritePosition(p.Y, p.X);
                    writerService.Write("S");
                }

                _isFirstDraw = false;
            }

            writerService.SetWritePosition(HeadCoordinates.Y, HeadCoordinates.X);
            writerService.Write("S");
            writerService.SetWritePosition(32, 0);
        }

        public bool MoveHead(Point newHeadCoordinates)
        {
            _lastRemovedPoint = null;
            var lastActivePoint = _activePoints.Last();
            if (newHeadCoordinates == HeadCoordinates || newHeadCoordinates == lastActivePoint)
            {
                return false;
            }

            _activePoints.Add(HeadCoordinates);
            HeadCoordinates = newHeadCoordinates;
            return true;
        }

        public void MoveTail()
        {
            _lastRemovedPoint = _activePoints[0];
            _activePoints.RemoveAt(0);
        }
    }
}
