using System;
using System.Configuration;
using System.ServiceModel;
using System.Diagnostics;
using System.Threading.Tasks;
using HMWTurnos;
using HUA.PCAAlephoo.Business.Helper;

namespace HUA.PCAAlephoo.Business.Modules.HIS
{
    public class HISTurnosModule
    {
        private readonly ITurnosService _turnosService;

        public HISTurnosModule()
        {
            _turnosService = new TurnosServiceClient(
                BindingFactory.BasicHttpsBindingFromAppConfig(),
                new EndpointAddress(ConfigurationManager.AppSettings["EndpointAddressHISTurnosService"]));

        }

        public async Task<DatosTurnoModelWrapper> ObtenerDatosTurno(long codigoPaciente, string tipoFormulario, string numeroFormulario)
        {
            try
            {
                var request = new ObtenerDatosTurno_000054CNSRequest
                {
                    codigoPaciente = codigoPaciente,
                    tipoFormulario = tipoFormulario,
                    numeroFormulario = numeroFormulario
                };
                var response = await _turnosService.ObtenerDatosTurno_000054CNSAsync(request);
                return response.ObtenerDatosTurno_000054CNSResult;
            }
            catch (Exception e)
            {
                return new DatosTurnoModelWrapper()
                {
                    EstadoRespuesta = new EstadoRespuesta
                    {
                        CodigoRespuesta = "170",
                        Mensaje = e.Message
                    }
                };
            }
            
        }

        public async Task<NovedadesTurnosModel> ObtenerNovedadesTurnos()
        {
            try
            {
                var response = await _turnosService.ObtenerNovedadesTurnos_000055CNSAsync(new ObtenerNovedadesTurnos_000055CNSRequest());
                return response.ObtenerNovedadesTurnos_000055CNSResult;
            }
            catch(Exception e)
            {
                return new NovedadesTurnosModel
                {
                    EstadoRespuesta =
                    {
                        CodigoRespuesta = "180",
                        Mensaje = e.Message
                    }
                };
            }
            
        }

        public async Task<EstadoRespuesta> ActualizarNovedadTurno(long idRegistro, long idProceso, string estado, string observaciones)
        {
            try
            {
                var request = new ActualizarNovedadTurno_000056ABMRequest()
                {
                    estado = estado,
                    idProceso = idProceso,
                    idRegistro = idRegistro,
                    observaciones = observaciones
                };

                var response = await _turnosService.ActualizarNovedadTurno_000056ABMAsync(request);
                return response.ActualizarNovedadTurno_000056ABMResult;
            }
            catch(Exception e)
            {
                return new EstadoRespuesta()
                {
                    Mensaje = e.Message,
                    CodigoRespuesta = "190"
                };
            }
            
        }
    }
}
