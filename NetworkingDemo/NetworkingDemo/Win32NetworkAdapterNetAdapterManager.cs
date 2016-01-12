using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingDemo
{
    public class Win32NetworkAdapterNetAdapterManager : INetAdapterManager
    {
        private string[] MSFTNetAdapterAttributes =
            new string[]
            {
                "AdapterType",
                "AdapterTypeID",
                "AutoSense",
                "Availability",
                "Caption",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CreationClassName",
                "Description",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "GUID",
                "Index",
                "InstallDate",
                "Installed",
                "InterfaceIndex",
                "LastErrorCode",
                "MACAddress",
                "Manufacturer",
                "MaxNumberControlled",
                "MaxSpeed",
                "Name",
                "NetConnectionID",
                "NetConnectionStatus",
                "NetEnabled",
                "NetworkAddresses[]",
                "PermanentAddress",
                "PhysicalAdapter",
                "PNPDeviceID",
                "PowerManagementCapabilities[]",
                "PowerManagementSupported",
                "ProductName",
                "ServiceName",
                "Speed",
                "Status",
                "StatusInfo",
                "SystemCreationClassName",
                "SystemName",
                "TimeOfLastReset"
            };

        public string NetAdaptersInfoReportGenerate()
        {
            var report = String.Empty;
            var reportStringBuilder = new StringBuilder();

            var scope = @"root\Cimv2";
            var queryText = "SELECT * FROM Win32_NetworkAdapter";

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

            var scope = @"root\Cimv2";
            var queryText = "SELECT * FROM Win32_NetworkAdapter";

            using (var searcher = new ManagementObjectSearcher(scope, queryText))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    var adapterTypeID = item["AdapterTypeID"];

                    if (adapterTypeID != null)
                    {
                        int ndisMedium = int.Parse(adapterTypeID.ToString());
                        if (ndisMedium == 0 || ndisMedium == 9)
                        {
                            item.InvokeMethod(method, null);
                        }
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
