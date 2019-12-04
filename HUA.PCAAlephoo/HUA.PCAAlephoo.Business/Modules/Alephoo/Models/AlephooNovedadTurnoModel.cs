using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HUA.PCAAlephoo.Business.Modules.Alephoo.Models
{
    public class AlephooNovedadTurnoModel
    {
        [JsonProperty("TurnoId")]
        public long TurnoId { get; set; }

        [JsonProperty("Usuario")]
        public string Usuario { get; set; }

        [JsonProperty("EstadoCodigo")]
        public string EstadoCodigo { get; set; }

        [JsonProperty("T0")]
        public string T0 { get; set; } // yyyy-mm-dd hh:mm

        [JsonProperty("T3")]
        public string T3 { get; set; } // yyyy-mm-dd hh:mm

        [JsonProperty("T4")]
        public string T4 { get; set; } // yyyy-mm-dd hh:mm

    }
}
