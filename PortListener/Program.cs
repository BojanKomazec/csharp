using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PortListener
{
    class Program
    {
        public static int MAX_PORT = 65535;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: portlistener <IPv4 address> <port>");
                return;
            }

            IPAddress ipAddress;
            string strIPAddress = args[0];
            if (!IPAddress.TryParse(args[0], out ipAddress))
            {
                Console.WriteLine("Input error: failed to parse first argument as ip address (IPv4)");
                return;
            }

            int port = 0;
            if (!int.TryParse(args[1], out port))
            {
                Console.WriteLine("Input error: failed to parse argument as int");
                return;
            }

            if (port < 1 || port > MAX_PORT)
            {
                Console.WriteLine("Input error: port must be in the range (1, 65535)");
                return;
            }            

            try
            {

                TcpListener tcpListener = new TcpListener(ipAddress, port);
                tcpListener.Start();

                Console.WriteLine("TCP socket {0}:{1} listening...", strIPAddress, port);
                Console.WriteLine("Press ENTER to terminate...");
                Console.ReadLine();
                tcpListener.Stop();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Failed to open port {0} on interface {1} for listening. Error: {2}", port, strIPAddress, exc.Message);
            }
        }
    }
}
