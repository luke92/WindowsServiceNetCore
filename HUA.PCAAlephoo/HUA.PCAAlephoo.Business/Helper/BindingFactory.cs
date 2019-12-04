using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Text;

namespace HUA.PCAAlephoo.Business.Helper
{
    public class BindingFactory
    {
        public static BasicHttpsBinding BasicHttpsBindingFromAppConfig()
        {
            var result = new System.ServiceModel.BasicHttpsBinding
            {
                Name = "BasicHttpsBinding_IService",
                CloseTimeout = new TimeSpan(0, 10, 0),
                OpenTimeout = new TimeSpan(0, 10, 0),
                ReceiveTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferPoolSize = int.MaxValue,
                ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max
            };
            try
            {
                var timeOut = ConfigurationManager.AppSettings["BindingTimeOut"];
                if (string.IsNullOrEmpty(timeOut)) return result;
                if (timeOut.Length != 8) return result;
                var time = timeOut.Split(":");
                if (time.Length != 3) return result;
                int.TryParse(time[0], out var hora);
                int.TryParse(time[1], out var minuto);
                int.TryParse(time[2], out var segundo);

                result.CloseTimeout = new TimeSpan(hora, minuto, segundo);
                result.OpenTimeout = new TimeSpan(hora, minuto, segundo);
                result.ReceiveTimeout = new TimeSpan(hora, minuto, segundo);
                result.SendTimeout = new TimeSpan(hora, minuto, segundo);
            }

            catch (Exception e)
            {
                Console.Write(e);
            }

            return result;

        }
    }
}
