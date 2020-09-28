using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Push
{
    /// <summary>
    /// Sends back server after PushCapsula receiving.
    /// </summary>
    public class PushResponse
    {
        /// <summary>
        /// Contains message IDs of self-sended messages.
        /// </summary>
        public IList<long> MessageIds { get; set; }
    }
}
