using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using HUA.PCAAlephoo.Business.Modules.Integracion;
using Timer = System.Threading.Timer;

namespace HUA.PCAAlephoo.WindowsService
{
    public class PCAAlephooWindowsService : ServiceBase
    {
        private Timer _timer;
        private int _interval = 1000;
        private readonly System.ComponentModel.IContainer components = null;
        private IntegracionModule integracionModule;
        private bool terminoDeProcesar = true;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.ServiceName = "PCAAlephooInterfazNovedadesTurnos";

            ((ISupportInitialize)this.EventLog).BeginInit();

            if (!EventLog.SourceExists(this.ServiceName))
            {
                EventLog.CreateEventSource(this.ServiceName, "Application");
            }

            ((ISupportInitialize)this.EventLog).EndInit();

            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = "Application";

            string intervaloAppConfig = ConfigurationManager.AppSettings["TiempoEsperaLlamadaEnSegundos"];

            if (int.TryParse(intervaloAppConfig, out var intervalo))
            {
                _interval = intervalo * 1000;
            }

            integracionModule = new IntegracionModule(EventLog);

        }

        public PCAAlephooWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);


            while (true)
            {
                // Se queda el servicio iniciado mientras se debugee la aplicación
            }


        }

        protected override void OnStart(string[] args)
        {
            this.EventLog.WriteEntry("Servicio " + ServiceName + " iniciado");
            SetTimer();
        }

        protected override void OnStop()
        {
            this.EventLog.WriteEntry("Servicio " + ServiceName + " iniciado");
        }

        private void Procesar()
        {
            terminoDeProcesar = false;
            try
            {

                integracionModule.ProcesarNovedades();
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.Message);
            }
            finally
            {
                terminoDeProcesar = true;
            }
        }

        public void SetTimer()
        {
            // this is System.Threading.Timer, of course
            _timer = new Timer(Tick, null, _interval, Timeout.Infinite);
        }

        private void Tick(object state)
        {
            try
            {
                if (terminoDeProcesar)
                    Procesar();
            }
            catch (Exception e)
            {
                this.EventLog.WriteEntry("Hubo un problema al procesar las novedades: " + e.Message);
            }
            finally
            {
                _timer?.Change(_interval, Timeout.Infinite);
            }
        }

    }
}
