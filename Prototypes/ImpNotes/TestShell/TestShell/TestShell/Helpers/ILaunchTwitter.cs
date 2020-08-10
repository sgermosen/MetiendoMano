using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Helpers
{
    public interface ILaunchTwitter
    {
        bool OpenUserName(string username);
        bool OpenStatus(string statusId);
    }
}
