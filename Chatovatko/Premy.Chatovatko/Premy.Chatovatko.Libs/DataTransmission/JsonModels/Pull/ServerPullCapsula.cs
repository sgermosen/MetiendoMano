using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Pull
{
    public class ServerPullCapsula
    {
        public IList<PullMessage>  Messages { get; set; }
        public IList<long> AesKeysUserIds { get; set; }
    }
}
