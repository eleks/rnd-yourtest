using System;
using System.Net;
using YourTest.Manager;

namespace YourTest.Droid.Manager
{
    public class IPAddressManager : IIPAddressManager
    {
        public String GetIPAddress()
        {
            IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());

            if (adresses != null && adresses[0] != null)
            {
                return adresses[0].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
