using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Premy.Chatovatko.Server.ClientListener
{
    public class GodotCounter
    {
        private ulong running = 0;
        private ulong destroyed = 0;
        private ulong created = 0;

        public ulong Created { get => created; }
        public ulong Destroyed { get => destroyed; }
        public ulong Running { get => running; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void IncreaseRunning()
        {
            running++;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void IncreaseCreated()
        {
            created++;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void IncreaseDestroyed()
        {
            destroyed++;
            running--;
        }
    }
}
