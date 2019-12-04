using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HUA.PCAAlephoo.Business.Modules.Alephoo;
using HUA.PCAAlephoo.Business.Modules.Alephoo.Models;
using HUA.PCAAlephoo.Business.Modules.HIS;

namespace HUA.PCAAlephoo.Business.Modules.Integracion
{
    public class IntegracionModule
    {
        private EventLog _eventLog;
        private readonly AlephooAdmisionModule _alephooAdmisionModule;
        private readonly HISTurnosModule _hisTurnosModule;
        public IntegracionModule(System.Diagnostics.EventLog eventLog)
        {
            _eventLog = eventLog;
            _alephooAdmisionModule = new AlephooAdmisionModule();
            _hisTurnosModule = new HISTurnosModule();
        }

        public async void ProcesarNovedades()
        {
            var novedades = await _hisTurnosModule.ObtenerNovedadesTurnos();

            if (novedades.EstadoRespuesta.CodigoRespuesta != "0")
            {
                _eventLog.WriteEntry("Error al obtener novedades de Turnos: " + novedades.EstadoRespuesta.CodigoRespuesta + " " + novedades.EstadoRespuesta.Mensaje);
                return;
            }

            foreach (var novedad in novedades.Novedades)
            {
                var turnoHis = await _hisTurnosModule.ObtenerDatosTurno(novedad.CodigoPaciente, novedad.TipoFormulario,
                    novedad.NumeroFormulario);

                if (turnoHis.EstadoRespuesta.CodigoRespuesta != "0")
                {
                    _eventLog.WriteEntry(String.Format("Error al obtener datos del turno: {0} {1}. ID:{2},IdProceso:{3},FechaNovedad:{4},TipoNovedad:{5},UsuarioNovedad:{6},CodigoPaciente:{7},TipoFormulario:{8},NumeroFormulario:{9}",
                        turnoHis.EstadoRespuesta.CodigoRespuesta, turnoHis.EstadoRespuesta.Mensaje, novedad.Id,
                        novedad.IdProceso, novedad.FechaNovedad, novedad.TipoNovedad, novedad.UsuarioNovedad,
                        novedad.CodigoPaciente, novedad.TipoFormulario, novedad.NumeroFormulario));
                    continue;
                }

                var turnoAlephoo = await _alephooAdmisionModule.ObtenerTurno(new FilterTurnoModel
                {
                    CodigoPaciente = novedad.CodigoPaciente,
                    TipoFormulario = novedad.TipoFormulario,
                    NumeroFormulario = novedad.NumeroFormulario
                });

                if (turnoAlephoo.EstadoRespuesta.CodigoRespuesta != "0")
                {
                    _eventLog.WriteEntry(String.Format("Error al obtener turno de Alephoo: {0} {1}. CodigoPaciente:{2},TipoFormulario:{3},NumeroFormulario:{4}",
                        turnoHis.EstadoRespuesta.CodigoRespuesta, turnoHis.EstadoRespuesta.Mensaje,
                        novedad.CodigoPaciente, novedad.TipoFormulario, novedad.NumeroFormulario));
                    continue;
                }

                var timer0 = !string.IsNullOrEmpty(turnoHis.DatosTurno.Timer0)
                    ? turnoAlephoo.Fecha + " " + turnoHis.DatosTurno.Timer0
                    : "";

                var timer3 = !string.IsNullOrEmpty(turnoHis.DatosTurno.Timer3)
                    ? turnoAlephoo.Fecha + " " + turnoHis.DatosTurno.Timer3
                    : "";

                var timer4 = !string.IsNullOrEmpty(turnoHis.DatosTurno.Timer4)
                    ? turnoAlephoo.Fecha + " " + turnoHis.DatosTurno.Timer4
                    : "";

                var informarNovedadAlephooResponse = await _alephooAdmisionModule.InformarNovedadTurno(new AlephooNovedadTurnoModel()
                {
                    EstadoCodigo = turnoHis.DatosTurno.CodigoEstadoRecepcion,
                    T0 = timer0,
                    T3 = timer3,
                    T4 = timer4,
                    TurnoId = turnoAlephoo.AlephooIdTurno,
                    Usuario = novedad.UsuarioNovedad
                });

                var estadoNovedad = informarNovedadAlephooResponse.CodigoRespuesta == "0" ? "A" : "R";

                var actualizarNovedadHisResponse = await _hisTurnosModule.ActualizarNovedadTurno(novedad.Id,
                    novedad.IdProceso, estadoNovedad, informarNovedadAlephooResponse.Mensaje);

                if (actualizarNovedadHisResponse.CodigoRespuesta != "0")
                {
                    _eventLog.WriteEntry(String.Format("Error al actualizar novedad del turno en HIS: {0} {1}. Id:{2},IdProceso:{3},EstadoNovedad:{4},Observacion:{5}",
                        actualizarNovedadHisResponse.CodigoRespuesta, actualizarNovedadHisResponse.Mensaje,novedad.Id,novedad.IdProceso,estadoNovedad,informarNovedadAlephooResponse.Mensaje));
                }
            }
        }
    }
}
