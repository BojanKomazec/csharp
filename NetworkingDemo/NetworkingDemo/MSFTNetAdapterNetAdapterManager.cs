using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace NetworkingDemo
{
    public class MSFTNetAdapterNetAdapterManager : INetAdapterManager
    {
        private string[] MSFTNetAdapterAttributes = 
            new string[] 
            {
                "Caption",
                "Description",
                "InstallDate",
                "Name",
                "Status",
                "Availability",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CreationClassName",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "LastErrorCode",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]",
                "PowerManagementSupported",
                "StatusInfo",
                "SystemCreationClassName",
                "SystemName",
                "Speed",
                "MaxSpeed",
                "RequestedSpeed",
                "UsageRestriction",
                "PortType",
                "OtherPortType",
                "OtherNetworkPortType",
                "PortNumber",
                "LinkTechnology",
                "OtherLinkTechnology",
                "PermanentAddress",
                //"NetworkAddresses[]",
                "FullDuplex",
                "AutoSense",
                "SupportedMaximumTransmissionUnit",
                "ActiveMaximumTransmissionUnit",
                "InterfaceDescription",
                "InterfaceName",
                "NetLuid",
                "InterfaceGuid",
                "InterfaceIndex",
                "DeviceName",
                "NetLuidIndex",
                "Virtual",
                "Hidden",
                "NotUserRemovable",
                "IMFilter",
                "InterfaceType",
                "HardwareInterface",
                "WdmInterface",
                "EndPointInterface",
                "iSCSIInterface",
                "State",
                "NdisMedium",
                "NdisPhysicalMedium",
                "InterfaceOperationalStatus",
                "OperationalStatusDownDefaultPortNotAuthenticated",
                "OperationalStatusDownMediaDisconnected",
                "OperationalStatusDownInterfacePaused",
                "OperationalStatusDownLowPowerState",
                "InterfaceAdminStatus",
                "MediaConnectState",
                "MtuSize",
                "VlanID",
                "TransmitLinkSpeed",
                "ReceiveLinkSpeed",
                "PromiscuousMode",
                "DeviceWakeUpEnable",
                "ConnectorPresent",
                "MediaDuplexState",
                "DriverDate",
                "DriverDateData",
                "DriverVersionString",
                "DriverName",
                "DriverDescription",
                "MajorDriverVersion",
                "MinorDriverVersion",
                "DriverMajorNdisVersion",
                "DriverMinorNdisVersion",
                "PnPDeviceID",
                "DriverProvider",
                "ComponentID",
                //"LowerLayerInterfaceIndices[]",
                //"HigherLayerInterfaceIndices[]",
                "AdminLocked"
            };

        public string NetAdaptersInfoReportGenerate()
        {
            var report = String.Empty;
            var reportStringBuilder = new StringBuilder();

            var scope = @"root\StandardCimv2";
            var queryText = "SELECT * FROM MSFT_NetAdapter";

            // var wmiQuery = new SelectQuery((queryText);

            using (var searcher = new ManagementObjectSearcher(scope, queryText))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    foreach(var attribute in this.MSFTNetAdapterAttributes)
                    {
                        try
                        {
                            reportStringBuilder.Append($"{attribute}: {item[attribute]?.ToString()}{Environment.NewLine}");
                        }
                        catch(ManagementException)
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
            var queryText = "SELECT * FROM MSFT_NetAdapter";

            using (var searcher = new ManagementObjectSearcher(scope, queryText))
            {
                foreach (ManagementObject item in searcher.Get())
                {

                    var NdisMedium = item["NdisMedium"];

                    if (NdisMedium != null)
                    {
                        int ndisMedium = int.Parse(NdisMedium.ToString());
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
