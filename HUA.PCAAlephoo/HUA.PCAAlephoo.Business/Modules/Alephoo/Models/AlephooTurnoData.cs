using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HUA.PCAAlephoo.Business.Modules.Alephoo.Models
{
    public class AlephooTurnoData
    {
        public long AlephooIdTurno { get; set; }
        public string Fecha { get; set; }
        public EstadoRespuesta EstadoRespuesta { get; set; }

        public AlephooTurnoData()
        {
            EstadoRespuesta = new EstadoRespuesta();
        }
    }

    public class AlephooTurnoDataWrapper
    {
        [JsonProperty("data")]
        public List<Data> Data { get; set; }

        /*
        [JsonProperty("included")]
        public List<Included> Included { get; set; }
        */
        

        public AlephooTurnoDataWrapper()
        {
            Data = new List<Data>();
            //Included = new List<Included>();
        }
    }

    public partial class Data
    {
        /*
        [JsonProperty("type")]
        public string Type { get; set; }
        */

        [JsonProperty("id")]
        public long Id { get; set; }

        
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
        /*
        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }
        */
        public Data()
        {
            Attributes = new Attributes();
            //Relationships = new Relationships();
        }
        
    }

    public class Attributes
    {
        public string Hora { get; set; }
        public string Fecha { get; set; }
        public int Orden { get; set; }
        public bool Sobreturno { get; set; }
        public string Observacion { get; set; }
        public bool EstaEnDiaNoHabil { get; set; }
        public bool EstaEnHoraNoHabil { get; set; }
        public string TipoFormulario { get; set; }
        public string NumeroFormulario { get; set; }
        public IList<string> NumerosAutorizaciones { get; set; }

        public Attributes()
        {
            NumerosAutorizaciones = new List<string>();
        }

    }

    public partial class Relationships
    {
        [JsonProperty("agenda")]
        public Agenda Agenda { get; set; }

        [JsonProperty("persona")]
        public Agenda Persona { get; set; }

        [JsonProperty("consulta")]
        public Consulta Consulta { get; set; }

        [JsonProperty("estadoTurno")]
        public Consulta EstadoTurno { get; set; }

        [JsonProperty("estudios")]
        public List<Consulta> Estudios { get; set; }
    }

    public partial class Agenda
    {
        [JsonProperty("data")]
        public DataClass Data { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }

    public partial class DataClass
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class Consulta
    {
        [JsonProperty("data")]
        public DataClass Data { get; set; }
    }

    public partial class Included
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("attributes")]
        public IncludedAttributes Attributes { get; set; }

        [JsonProperty("relationships", NullValueHandling = NullValueHandling.Ignore)]
        public IncludedRelationships Relationships { get; set; }
    }

    public partial class IncludedAttributes
    {
        [JsonProperty("nombre", NullValueHandling = NullValueHandling.Ignore)]
        public string Nombre { get; set; }

        [JsonProperty("codigo", NullValueHandling = NullValueHandling.Ignore)]
        public string Codigo { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("nombres", NullValueHandling = NullValueHandling.Ignore)]
        public string Nombres { get; set; }

        [JsonProperty("apellidos", NullValueHandling = NullValueHandling.Ignore)]
        public string Apellidos { get; set; }

        [JsonProperty("nroHc", NullValueHandling = NullValueHandling.Ignore)]
        public long? NroHc { get; set; }

        [JsonProperty("preparacion")]
        public object Preparacion { get; set; }

        [JsonProperty("itemId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ItemId { get; set; }

        [JsonProperty("autorizacionAgrupable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AutorizacionAgrupable { get; set; }

        [JsonProperty("restringeItems", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RestringeItems { get; set; }

        [JsonProperty("numeroManual", NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroManual { get; set; }

        [JsonProperty("cantidad", NullValueHandling = NullValueHandling.Ignore)]
        public long? Cantidad { get; set; }

        [JsonProperty("documento", NullValueHandling = NullValueHandling.Ignore)]
        public string Documento { get; set; }

        [JsonProperty("legajo", NullValueHandling = NullValueHandling.Ignore)]
        public string Legajo { get; set; }

        [JsonProperty("esGrupal", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EsGrupal { get; set; }

        [JsonProperty("esUrgencia", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EsUrgencia { get; set; }

        [JsonProperty("esEquipo", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EsEquipo { get; set; }

        [JsonProperty("estudioNombre", NullValueHandling = NullValueHandling.Ignore)]
        public string EstudioNombre { get; set; }

        [JsonProperty("estudioId", NullValueHandling = NullValueHandling.Ignore)]
        public long? EstudioId { get; set; }

        [JsonProperty("estudioCodigo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? EstudioCodigo { get; set; }

        [JsonProperty("comentario", NullValueHandling = NullValueHandling.Ignore)]
        public string Comentario { get; set; }

        [JsonProperty("sobreturnos")]
        public object Sobreturnos { get; set; }

        [JsonProperty("dia", NullValueHandling = NullValueHandling.Ignore)]
        public long? Dia { get; set; }

        [JsonProperty("horaInicio", NullValueHandling = NullValueHandling.Ignore)]
        public string HoraInicio { get; set; }

        [JsonProperty("horaFin", NullValueHandling = NullValueHandling.Ignore)]
        public string HoraFin { get; set; }

        [JsonProperty("inicioVigencia", NullValueHandling = NullValueHandling.Ignore)]
        public Vigencia InicioVigencia { get; set; }

        [JsonProperty("finVigencia", NullValueHandling = NullValueHandling.Ignore)]
        public Vigencia FinVigencia { get; set; }

        [JsonProperty("duracionTurno", NullValueHandling = NullValueHandling.Ignore)]
        public long? DuracionTurno { get; set; }

        [JsonProperty("limitePacientes", NullValueHandling = NullValueHandling.Ignore)]
        public long? LimitePacientes { get; set; }

        [JsonProperty("frecuencia", NullValueHandling = NullValueHandling.Ignore)]
        public long? Frecuencia { get; set; }

        [JsonProperty("aDemanda", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ADemanda { get; set; }

        [JsonProperty("porCallcenter", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PorCallcenter { get; set; }

        [JsonProperty("cantPorCallcenter", NullValueHandling = NullValueHandling.Ignore)]
        public long? CantPorCallcenter { get; set; }

        [JsonProperty("cantidadTurnos", NullValueHandling = NullValueHandling.Ignore)]
        public long? CantidadTurnos { get; set; }

        [JsonProperty("limiteDias")]
        public object LimiteDias { get; set; }

        [JsonProperty("piso", NullValueHandling = NullValueHandling.Ignore)]
        public string Piso { get; set; }
    }

    public partial class Vigencia
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("timezone_type")]
        public long TimezoneType { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }

    public partial class IncludedRelationships
    {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Consulta> Items { get; set; }

        [JsonProperty("tipoPrestacion", NullValueHandling = NullValueHandling.Ignore)]
        public Consulta TipoPrestacion { get; set; }

        [JsonProperty("estudio", NullValueHandling = NullValueHandling.Ignore)]
        public Agenda Estudio { get; set; }

        [JsonProperty("autorizacion", NullValueHandling = NullValueHandling.Ignore)]
        public Consulta Autorizacion { get; set; }

        [JsonProperty("persona", NullValueHandling = NullValueHandling.Ignore)]
        public Agenda Persona { get; set; }

        [JsonProperty("profesional", NullValueHandling = NullValueHandling.Ignore)]
        public Agenda Profesional { get; set; }

        [JsonProperty("especialidad", NullValueHandling = NullValueHandling.Ignore)]
        public Agenda Especialidad { get; set; }

        [JsonProperty("agendaEstudios", NullValueHandling = NullValueHandling.Ignore)]
        public List<Consulta> AgendaEstudios { get; set; }

        [JsonProperty("institucion", NullValueHandling = NullValueHandling.Ignore)]
        public Agenda Institucion { get; set; }

        [JsonProperty("asignacion", NullValueHandling = NullValueHandling.Ignore)]
        public Agenda Asignacion { get; set; }
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(long) || t == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

}
