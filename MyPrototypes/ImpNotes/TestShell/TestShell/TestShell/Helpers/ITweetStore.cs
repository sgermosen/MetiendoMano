using System;
using System.Collections.Generic;
using System.Text;
using TestShell.Models;

namespace TestShell.Helpers
{
    public interface ITweetStore
    {
        void Save(System.Collections.Generic.List<Tweet> tweets);
        //System.Collections.Generic.List<Hanselman.Shared.Tweet> Load ();
    }
}
