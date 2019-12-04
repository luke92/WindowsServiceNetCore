using System;
using System.Collections.Generic;
using System.Text;

namespace HUA.PCAAlephoo.Business.Modules.Alephoo.Models
{
    public class EstadoRespuesta
    {
        public string CodigoRespuesta { get; set; }
        public string Mensaje { get; set; }

        public EstadoRespuesta()
        {
            CodigoRespuesta = "0";
            Mensaje = "";
        }
    }

    public class EstadoRespuestaWrapper
    {
        public EstadoRespuesta EstadoRespuesta { get; set; }
    }

}
