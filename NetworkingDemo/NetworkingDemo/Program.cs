using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var netAdapterManager = new Win32NetworkAdapterNetAdapterManager();
                var report = netAdapterManager.NetAdaptersInfoReportGenerate();
                Console.WriteLine(report);

                Console.WriteLine("Press ENTER to disconnect...");
                Console.ReadLine();

                netAdapterManager.InternetDisconnect();

                Console.WriteLine("Adapters after disconnecting...");
                report = netAdapterManager.NetAdaptersInfoReportGenerate();
                Console.WriteLine(report);

                Console.WriteLine("Press ENTER to connect...");
                Console.ReadLine();

                netAdapterManager.InternetConnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.ToString()}");
            }

            Console.WriteLine("Press ENTER to return...");
            Console.ReadLine();
        }
    }
}
