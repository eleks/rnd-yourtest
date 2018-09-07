using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using YourTest.Manager;
using System.Linq;

namespace YourTest.iOS.Manager
{
    public class IPAddressManager : IIPAddressManager
    {
        public String GetIPAddress()
        {
            String ipAddress = "";

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    var addresses = netInterface.GetIPProperties().UnicastAddresses.Select(a => a.Address.ToString());
                    var address = addresses.FirstOrDefault(a => a.StartsWith("172"));

                    if (!String.IsNullOrEmpty(address))
                    {
                        return address;
                    }

                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addrInfo.Address.ToString();

                        }
                    }
                }
            }

            return ipAddress;
        }
    }
}
