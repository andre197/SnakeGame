namespace Snake.Domain.Services
{
    using System;
    using Snake.Domain.Enums;

    public interface ILowLevelEventsService : IDisposable
    {
        event EventHandler<Keys> OnKeyPressed;

        event EventHandler<Keys> OnKeyUnpressed;

        void Subscribe();

        void Unsubscribe();
    }
}
