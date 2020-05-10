namespace Snake.Domain.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Snake.Domain.Enums;

    public class HighLevelEventsService : IHighLevelEventsService
    {
        private readonly ILowLevelEventsService _lowLevelEventsService;
        private bool _controlPressed;

        public event EventHandler<MoveKey> Move;
        public event EventHandler SpaceBarPressed;
        public event EventHandler Save;
        public event EventHandler Exit;

        public HighLevelEventsService(ILowLevelEventsService lowLevelEventsService)
        {
            _lowLevelEventsService = lowLevelEventsService;
        }

        public void Start()
        {
            _lowLevelEventsService.Subscribe();
            _lowLevelEventsService.OnKeyPressed -= OnKeyPressed;
            _lowLevelEventsService.OnKeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, Keys e)
        {
            switch (e)
            {
                case Keys.Up:
                    Move?.Invoke(this, MoveKey.Up);
                    _controlPressed = false;
                    break;
                case Keys.Down:
                    Move?.Invoke(this, MoveKey.Down);
                    _controlPressed = false;
                    break;
                case Keys.Left:
                    Move?.Invoke(this, MoveKey.Left);
                    _controlPressed = false;
                    break;
                case Keys.Right:
                    Move?.Invoke(this, MoveKey.Right);
                    _controlPressed = false;
                    break;
                case Keys.LControlKey:
                case Keys.RControlKey:
                    _controlPressed = true;
                    break;
                case Keys.S:
                    if (_controlPressed)
                    {
                        Save?.Invoke(this, EventArgs.Empty);
                        _controlPressed = false;
                    }
                    break;
                case Keys.X:
                    if (_controlPressed)
                    {
                        Exit?.Invoke(this, EventArgs.Empty);
                        _controlPressed = false;
                    }
                    break;
                case Keys.Space:
                    SpaceBarPressed?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    _controlPressed = false;
                    break;
            }
        }

        public void Dispose()
        {
            _lowLevelEventsService.Dispose();
        }

        public void Stop()
        {
            _lowLevelEventsService.Unsubscribe();
        }
    }
}
