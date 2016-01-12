using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingDemo
{
    public class Win32NetworkAdapterConfigurationNetAdapterManager : INetAdapterManager
    {
        private string[] MSFTNetAdapterAttributes =
            new string[]
            {
                "ArpUseEtherSNAP",
                "ArpAlwaysSourceRoute",
                "Caption",
                "DatabasePath",
                "DeadGWDetectEnabled",
                //"DefaultIPGateway[]",
                "DefaultTOS",
                "DefaultTTL",
                "Description",
                "DHCPEnabled",
                "DHCPLeaseExpires",
                "DHCPLeaseObtained",
                "DHCPServer",
                "DNSDomain",
                //"DNSDomainSuffixSearchOrder[]",
                "DNSEnabledForWINSResolution",
                "DNSHostName",
                //"DNSServerSearchOrder[]",
                "DomainDNSRegistrationEnabled",
                "ForwardBufferMemory",
                "FullDNSRegistrationEnabled",
                //"GatewayCostMetric[]",
                "IGMPLevel",
                "Index",
                "InterfaceIndex",
                //"IPAddress[]",
                "IPConnectionMetric",
                "IPEnabled",
                "IPFilterSecurityEnabled",
                "IPPortSecurityEnabled",
                //"IPSecPermitIPProtocols[]",
                //"IPSecPermitTCPPorts[]",
                //"IPSecPermitUDPPorts[]",
                //"IPSubnet[]",
                "IPUseZeroBroadcast",
                "IPXAddress",
                "IPXEnabled",
                //"IPXFrameType[]",
                "IPXMediaType",
                //"IPXNetworkNumber[]",
                "IPXVirtualNetNumber",
                "KeepAliveInterval",
                "KeepAliveTime",
                "MACAddress",
                "MTU",
                "NumForwardPackets",
                "PMTUBHDetectEnabled",
                "PMTUDiscoveryEnabled",
                "ServiceName",
                "SettingID",
                "TcpipNetbiosOptions",
                "TcpMaxConnectRetransmissions",
                "TcpMaxDataRetransmissions",
                "TcpNumConnections",
                "TcpUseRFC1122UrgentPointer",
                "TcpWindowSize",
                "WINSEnableLMHostsLookup",
                "WINSHostLookupFile",
                "WINSPrimaryServer",
                "WINSScopeID",
                "WINSSecondaryServer"
            };

        public string NetAdaptersInfoReportGenerate()
        {
            var report = String.Empty;
            var reportStringBuilder = new StringBuilder();

            var scope = @"root\StandardCimv2";
            var queryText = "SELECT * FROM Win32_NetworkAdapterConfiguration";

            // var wmiQuery = new SelectQuery((queryText);

            using (var searcher = new ManagementObjectSearcher(scope, queryText))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    foreach (var attribute in this.MSFTNetAdapterAttributes)
                    {
                        try
                        {
                            reportStringBuilder.Append($"{attribute}: {item[attribute]?.ToString()}{Environment.NewLine}");
                        }
                        catch (ManagementException)
                        {
                            Console.WriteLine($"Attribute \"{attribute}\" NOT FOUND");
                        }
                    }

                    reportStringBuilder.Append(Environment.NewLine);
                };
            }

            return reportStringBuilder.ToString();
        }

        private void NetworkAdapterEnable(bool enable)
        {
            var method = enable ? "Enable" : "Disable";

            var scope = @"root\StandardCimv2";
            var queryText = "SELECT * FROM Win32_NetworkAdapterConfiguration";

            using (var searcher = new ManagementObjectSearcher(scope, queryText))
            {
                foreach (ManagementObject item in searcher.Get())
                {

                    int ndisMedium = int.Parse(item["NdisMedium"].ToString());
                    if (ndisMedium == 0 || ndisMedium == 9)
                    {
                        item.InvokeMethod(method, null);
                    }
                };
            }
        }

        public void InternetConnect()
        {
            this.NetworkAdapterEnable(true);
        }

        public void InternetDisconnect()
        {
            this.NetworkAdapterEnable(false);
        }
    }
}
