namespace Snake.Domain.Models
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Enums;
    using Domain.PauseTokens;
    using Domain.Services;

    public class Game : IDisposable
    {
        private readonly object _lockObject = new object();

        private readonly IHighLevelEventsService _highLevelEventsService;
        private readonly IReaderService _readerService;
        private readonly IWriterService _writerService;
        private readonly ISettingsSaver _settingsSaver;
        private readonly INewPointFactory _newPointFactory;
        private readonly ILoadSettingsStrategyFactory _loadSettingsStrategyFactory;

        private Snake _snake;
        private GameArea _gameArea;
        private GameSettings _gameSettings = new GameSettings();
        private MoveKey _lastMoveKeyPressed = MoveKey.Right;
        private bool _skipAutoMove;
        private Point _foodPosition;
        private Task _game;
        private CancellationTokenSource _cancellationToken;
        private int _score = 0;

        public Game(
            IHighLevelEventsService highLevelEventsService,
            IReaderService readerService,
            IWriterService writerService,
            ISettingsSaver settingsSaver,
            INewPointFactory newPointFactory,
            ILoadSettingsStrategyFactory loadSettingsStrategyFactory)
        {
            _highLevelEventsService = highLevelEventsService;
            _readerService = readerService;
            _writerService = writerService;
            _settingsSaver = settingsSaver;
            _newPointFactory = newPointFactory;
            _loadSettingsStrategyFactory = loadSettingsStrategyFactory;

            _highLevelEventsService.SpaceBarPressed += PauseResume;
            _highLevelEventsService.Save += Save;
            _highLevelEventsService.Exit += Exit;
            _highLevelEventsService.Move += Move;
        }

        public PauseTokenSource PauseTokenSource { get; private set; } = new PauseTokenSource();

        public void Start()
        {
            _writerService.WriteLine("Welcome to snake game!");

            while (true)
            {
                if (_game != null)
                {
                    break;
                }

                _writerService.WriteLine("Press enter to start. Type help to get help for the game!");
                var input = _readerService.Read().ToLower();
                var result = _loadSettingsStrategyFactory.GetStrategy(input).LoadSettings();
                if (!result.Loaded || result.GameSettings == null)
                {
                    continue;
                }

                _cancellationToken = new CancellationTokenSource();
                _gameSettings = result.GameSettings;
                _game = BeginGame(_gameSettings, PauseTokenSource.Token, _cancellationToken.Token);
                Task.WaitAll(_game);

                _writerService.Clear();
                _highLevelEventsService.Stop();
                _writerService.WriteLine("You just died. If you want to restart press r");
                var readInfo = _readerService.Read();
                if (readInfo.ToLower() != "r")
                {
                    break;
                }

                _game = null;
                _score = 0;
            }
        }

        public void Dispose()
        {
            _highLevelEventsService.Dispose();
        }

        private async Task BeginGame(GameSettings gameSettings, PauseToken pauseToken, CancellationToken cancellationToken)
        {
            var snakeBody = gameSettings.Snake.Take(gameSettings.Snake.Count - 1);
            var snakeHead = gameSettings.Snake.Last();

            _snake = new Snake(snakeBody, snakeHead);
            _gameArea = new GameArea(new Point(0, 0), new Point(gameSettings.GameAreaSize.Width, gameSettings.GameAreaSize.Height));

            _gameArea.Draw(_writerService);
            _snake.Draw(_writerService);

            SpawnNewFood();
            WriteScore();

            _highLevelEventsService.Start();

            var delayInMs = _gameSettings.GetSpeedInMs();

            while (true)
            {
                await pauseToken.WaitWhilePausedAsync();
                if (_skipAutoMove)
                {
                    _skipAutoMove = false;
                    continue;
                }

                Move(this, _lastMoveKeyPressed);

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await Task.Delay(delayInMs);
            }

            _highLevelEventsService.Stop();
        }

        private void PauseResume(object sender, EventArgs e)
        {
            PauseTokenSource.IsPaused = !PauseTokenSource.IsPaused;
        }

        private void Exit(object sender, EventArgs e)
        {
            Dispose();
            Environment.Exit(0);
        }

        private void Save(object sender, EventArgs e)
        {
            _gameSettings.Snake = _snake.ActivePoints.ToList();
            _settingsSaver.Save(_gameSettings);
        }

        private void Move(object sender, MoveKey e)
        {
            lock (_lockObject)
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                Point newPoint = _newPointFactory.GetNewPoint(e, _snake.HeadCoordinates);
                var result = _snake.MoveHead(newPoint);
                if (!result)
                {
                    return;
                }

                _lastMoveKeyPressed = e;
                _skipAutoMove = true;
                if (newPoint != _foodPosition)
                {
                    _snake.MoveTail();
                }
                else
                {
                    _score++;
                    WriteScore();
                    SpawnNewFood();
                }

                _snake.Draw(_writerService);

                if (_snake.HasBittenHerself || _gameArea.IsPointOnEdgeOfGameArea(_snake.HeadCoordinates))
                {
                    _cancellationToken.Cancel();
                }
            }
        }

        private void WriteScore()
        {
            _writerService.SetWritePosition(33, 0);
            _writerService.WriteLine($"score: {_score:D5}");
        }

        private void SpawnNewFood()
        {
            var random = new Random();
            while (true)
            {
                var x = random.Next(_gameArea.UpperLeftCoordinates.X + 1, _gameArea.BottomRightCoordinates.X - 1);
                var y = random.Next(_gameArea.UpperLeftCoordinates.Y + 1, _gameArea.BottomRightCoordinates.Y - 1);

                var foodPoint = new Point(x, y);

                if (_snake.ActivePoints.Any(p => p == foodPoint) || _snake.HeadCoordinates == foodPoint)
                {
                    continue;
                }

                _foodPosition = foodPoint;
                _writerService.SetWritePosition(foodPoint.Y, foodPoint.X);
                _writerService.Write("*");
                break;
            }
        }
    }
}
