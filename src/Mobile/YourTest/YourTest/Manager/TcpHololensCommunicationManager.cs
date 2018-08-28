using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourTest.Manager
{
    public class TcpHololensCommunicationManager : IHololensCommunicationManager
    {
        public event EventHandler<String> MessageReceived;

        public void StartListening(String address, Int32 port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(address), port);
                listener.Start();
                Task.Run(() => Loop());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void StopListening()
        {
            if (listener != null)
                listener.Stop();
        }

        private void Loop()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread clientThread = new Thread(new ThreadStart(() =>
                {
                    NetworkStream stream = null;
                    try
                    {
                        stream = client.GetStream();
                        byte[] data = new byte[64];
                        while (true)
                        {
                            var builder = new StringBuilder();
                            int bytes = 0;
                            do
                            {
                                bytes = stream.Read(data, 0, data.Length);
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                            }
                            while (stream.DataAvailable);

                            string message = builder.ToString();
                            MessageReceived?.Invoke(this, message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (stream != null)
                            stream.Close();
                        if (client != null)
                            client.Close();
                    }
                }));

                clientThread.Start();
            }
        }

        private TcpListener listener;
    }
}
