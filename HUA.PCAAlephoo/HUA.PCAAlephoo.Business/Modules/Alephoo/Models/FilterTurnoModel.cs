using System;
using System.Collections.Generic;
using System.Text;

namespace HUA.PCAAlephoo.Business.Modules.Alephoo.Models
{
    public class FilterTurnoModel
    {
        public long CodigoPaciente { get; set; }
        public string TipoFormulario { get; set; }
        public string NumeroFormulario { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public bool? SobreTurno { get; set; }
        public bool? NoCancelado { get; set; }
        public string EspecialidadCodigo { get; set; }
        public string InstitucionCodigo { get; set; }
        public string ProfesionalCodigo { get; set; }

        public string GetStringSearchFilter()
        {
            if (!string.IsNullOrEmpty(TipoFormulario) && !string.IsNullOrEmpty(NumeroFormulario))
                return String.Format("filter[codigoAdHoc]={0}_{1}", TipoFormulario, NumeroFormulario);

            return "";
        }
    }
}
