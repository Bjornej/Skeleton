using System;

namespace Web.Windsor
{
    internal class DisposeAction : IDisposable
    {
        private readonly Action _action;

        public DisposeAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
