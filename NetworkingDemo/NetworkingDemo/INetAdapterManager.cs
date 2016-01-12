using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingDemo
{
    public interface INetAdapterManager
    {
        string NetAdaptersInfoReportGenerate();
        void InternetConnect();
        void InternetDisconnect();
    }
}
