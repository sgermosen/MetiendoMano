using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public class JsonCapsula
    {
        public JsonCapsula()
        {
            Attechment = null;
        }
        public JsonCapsula(IJType message)
        {
            this.Message = message;
            this.Attechment = null;
        }
        public IJType Message { get; set; }
        public byte[] Attechment { get; set; }

    }
}
