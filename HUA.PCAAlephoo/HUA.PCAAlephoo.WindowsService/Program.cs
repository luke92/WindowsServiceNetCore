using System.Diagnostics;
using System.ServiceProcess;
using System.Linq;

namespace HUA.PCAAlephoo.WindowsService
{
    public class Program
    {
        static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            using (var service = new PCAAlephooWindowsService())
            {
                if (isService)
                {
                    ServiceBase.Run(service);
                }
                else
                {
                    service.OnDebug();
                }

            }
        }
    }
}
