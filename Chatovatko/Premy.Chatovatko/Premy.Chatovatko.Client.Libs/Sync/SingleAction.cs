using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Sync
{
    public class SingleAction : IAction
    {
        private readonly Action action;
        public SingleAction(Action action)
        {
            this.action = action;
        }
        public IAction GetNext()
        {
            return null;
        }

        public bool Run()
        {
            action();
            return false;
        }
    }
}
