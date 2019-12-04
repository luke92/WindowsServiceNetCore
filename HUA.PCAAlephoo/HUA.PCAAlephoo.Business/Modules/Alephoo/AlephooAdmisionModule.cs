using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HUA.PCAAlephoo.Business.Helper;
using HUA.PCAAlephoo.Business.Modules.Alephoo.Models;
using Newtonsoft.Json;

namespace HUA.PCAAlephoo.Business.Modules.Alephoo
{
    public class AlephooAdmisionModule
    {
        private readonly HttpClient _clientNST;

        public AlephooAdmisionModule()
        {
            string username = ConfigurationManager.AppSettings["NST_Alephoo_Auth_Username"];
            string password = ConfigurationManager.AppSettings["NST_Alephoo_Auth_Password"];
            string proxyServer = ConfigurationManager.AppSettings["ProxyServer"];
            string baseAddress = ConfigurationManager.AppSettings["NST_Alephoo_API_URI"];

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            if (ConfigurationManager.AppSettings["EnableProxy"].ToLower().Equals("true"))
            {
                handler.UseProxy = true;
                handler.Proxy = new WebProxy(proxyServer);
            }

            _clientNST = new HttpClient(handler);
            _clientNST.BaseAddress = new Uri(baseAddress);
            _clientNST.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _clientNST.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            var byteArray = Encoding.ASCII.GetBytes(String.Format("{0}:{1}",username,password));
            _clientNST.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            // trust any certificate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<EstadoRespuesta> InformarNovedadTurno(AlephooNovedadTurnoModel alephooNovedadTurnoModel)
        {
            EstadoRespuesta estadoRespuesta = new EstadoRespuesta();

            try
            {
                string url = "admision/turnos/customFormat";

                var response = await HttpClientEx.PatchJsonAsync(_clientNST, url, typeof(AlephooNovedadTurnoModel),
                    alephooNovedadTurnoModel);

                if (!response.IsSuccessStatusCode)
                {
                    var mensajeError = "Error con la conexión del Sistema de Turnos: " + ((int)response.StatusCode) + " " + response.StatusCode;
                    estadoRespuesta.CodigoRespuesta = response.StatusCode.ToString();
                    estadoRespuesta.Mensaje = mensajeError;
                }

                var stringResponse = await response.Content.ReadAsStringAsync();
                var json = removeErrorsServer(stringResponse);
                var estado = JsonConvert.DeserializeObject<EstadoRespuestaWrapper>(json);
                estadoRespuesta.CodigoRespuesta = estado.EstadoRespuesta.CodigoRespuesta;
                estadoRespuesta.Mensaje = estado.EstadoRespuesta.Mensaje;
            }
            catch (Exception e)
            {
                estadoRespuesta.CodigoRespuesta = "150";
                estadoRespuesta.Mensaje = e.Message;
            }

            return estadoRespuesta;

        }

        public async Task<AlephooTurnoData> ObtenerTurno(FilterTurnoModel filterTurnoModel)
        {
            AlephooTurnoData turnoData = new AlephooTurnoData();

            try
            {
                string url = "admision/turnos?";

                url += filterTurnoModel.GetStringSearchFilter();

                var response = await _clientNST.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var mensajeError = "Error con la conexión del Sistema de Turnos: " + ((int)response.StatusCode) + " " + response.StatusCode;
                    turnoData.EstadoRespuesta.CodigoRespuesta = response.StatusCode.ToString();
                    turnoData.EstadoRespuesta.Mensaje = mensajeError;
                }
                else
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var json = removeErrorsServer(stringResponse);
                    var turnoAlephooData = JsonConvert.DeserializeObject<AlephooTurnoDataWrapper>(json);
                    turnoData.AlephooIdTurno = turnoAlephooData.Data[0].Id;
                    turnoData.Fecha = turnoAlephooData.Data[0].Attributes.Fecha;
                    turnoData.EstadoRespuesta = new EstadoRespuesta();
                }
            }
            catch (Exception e)
            {
                turnoData.EstadoRespuesta.CodigoRespuesta = "160";
                turnoData.EstadoRespuesta.Mensaje = e.Message;
            }

            return turnoData;
        }

        private string removeErrorsServer(string responseString)
        {
            string output = "";
            if (responseString.Length > 0)
            {
                if (responseString.IndexOf('{') > -1)
                {
                    output = responseString.Substring(responseString.IndexOf('{'));
                }
            }

            return output;
        }
    }
}
