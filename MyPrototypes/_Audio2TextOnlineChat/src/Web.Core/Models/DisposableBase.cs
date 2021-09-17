using System;

namespace Web.Core.Models
{
    public abstract class DisposableBase : IDisposable
    {
        protected virtual void Dispose(bool isDisposing) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
