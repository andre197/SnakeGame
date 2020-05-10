namespace Snake.Domain.PauseTokens
{
    using System.Threading;
    using System.Threading.Tasks;

    public class PauseTokenSource
    {
        private volatile TaskCompletionSource<bool> _paused;

        internal static readonly Task CompletedTask = Task.FromResult(true);
        
        public bool IsPaused
        {
            get { return _paused != null; }
            set
            {
                if (value)
                {
                    Interlocked.CompareExchange(
                        ref _paused, new TaskCompletionSource<bool>(), null);
                }
                else
                {
                    while (true)
                    {
                        var tcs = _paused;
                        if (tcs == null)
                        {
                            return;
                        }

                        if (Interlocked.CompareExchange(ref _paused, null, tcs) == tcs)
                        {
                            tcs.SetResult(true);
                            break;
                        }
                    }
                }
            }
        }

        public PauseToken Token => new PauseToken(this);

        internal Task WaitWhilePausedAsync()
        {
            var cur = _paused;
            return cur != null ? cur.Task : CompletedTask;
        }
    }
}
