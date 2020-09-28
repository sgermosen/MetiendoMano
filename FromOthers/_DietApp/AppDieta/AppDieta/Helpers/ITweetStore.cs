using AppDieta.Models;
using System;


namespace AppDieta
{
    public interface ITweetStore
    {
        void Save(System.Collections.Generic.List<Tweet> tweets);
        //System.Collections.Generic.List<Hanselman.Shared.Tweet> Load ();
    }
}

