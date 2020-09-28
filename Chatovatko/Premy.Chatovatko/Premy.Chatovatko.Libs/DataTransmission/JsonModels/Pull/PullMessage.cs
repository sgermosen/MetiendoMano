using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Pull
{
    public class PullMessage
    {
        public long PublicId { get; set; }
        public long SenderId { get; set; }
    }
}
