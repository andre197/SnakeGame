namespace Snake.Domain.PauseTokens
{
    using System.Threading.Tasks;

    public struct PauseToken
    {
        private readonly PauseTokenSource _source;

        internal PauseToken(PauseTokenSource source)
        {
            _source = source;
        }

        public bool IsPaused { get { return _source != null && _source.IsPaused; } }

        public Task WaitWhilePausedAsync()
        {
            return IsPaused ?
                _source.WaitWhilePausedAsync() :
                PauseTokenSource.CompletedTask;
        }
    }
}