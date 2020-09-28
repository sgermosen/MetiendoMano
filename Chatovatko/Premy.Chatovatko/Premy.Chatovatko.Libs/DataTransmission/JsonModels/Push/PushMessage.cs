using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Push
{
    public class PushMessage
    {
        public int RecepientId { get; set; }
        public int Priority { get; set; }
    }
}
