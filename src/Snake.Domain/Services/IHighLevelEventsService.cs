namespace Snake.Domain.Services
{
    using System;
    using Snake.Domain.Enums;

    public interface IHighLevelEventsService : IDisposable
    {
        event EventHandler<MoveKey> Move;
        event EventHandler SpaceBarPressed;
        event EventHandler Save;
        event EventHandler Exit;

        void Start();
        void Stop();
    }
}
