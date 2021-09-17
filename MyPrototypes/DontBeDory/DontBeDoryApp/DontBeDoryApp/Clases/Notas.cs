using System;
using System.Collections.Generic;
using System.Text;

namespace DontBeDoryApp.Clases
{
    public class Notas
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string NombreNota { get; set; }
        public string NombreCuerpo { get; set; }
    }
}
