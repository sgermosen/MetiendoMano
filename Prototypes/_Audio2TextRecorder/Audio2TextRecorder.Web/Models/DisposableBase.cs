using System;

namespace Audio2TextRecorder.Web.Models
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
