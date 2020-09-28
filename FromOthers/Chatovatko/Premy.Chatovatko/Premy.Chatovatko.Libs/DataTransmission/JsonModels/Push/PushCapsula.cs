using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Push
{
    public class PushCapsula
    {
        public PushCapsula()
        {
            PushMessages = new List<PushMessage>();
            MessageToDeleteIds = new List<long>();
        }

        public IList<PushMessage> PushMessages { get; set; }
        public IList<long> MessageToDeleteIds { get; set; }
    }
}
