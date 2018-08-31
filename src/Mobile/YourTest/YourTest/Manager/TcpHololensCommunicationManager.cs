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
        public event EventHandler ClientConnected;

        public void StartListening(String address, Int32 port)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Parse(address), port);
                _listener.Start();
                _listeningCts = new CancellationTokenSource();
                var t = LoopAsync(_listeningCts.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void StopListening()
        {
            _listeningCts?.Cancel();
            _listener?.Stop();
            _listener = null;
            _listeningCts = null;
        }

        private async Task LoopAsync(CancellationToken cancellationToken)
        {

            TcpClient client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
            ClientConnected?.Invoke(this, EventArgs.Empty);

            await ReadStreamAsync(client, cancellationToken);
        }

        private async Task ReadStreamAsync(TcpClient client, CancellationToken cancellationToken)
        {
            await Task.Yield();

            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                while (!cancellationToken.IsCancellationRequested)
                {
                    var builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

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
                stream?.Close();
                stream = null;
                client?.Close();
                client = null;

            }
        }

        private TcpListener _listener;
        private CancellationTokenSource _listeningCts;
    }
}
