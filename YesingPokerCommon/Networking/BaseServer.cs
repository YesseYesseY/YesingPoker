using System.Net;
using System.Net.Sockets;

namespace YesingPokerCommon.Networking
{
    public class BaseServer
    {
        public static readonly int MAX_MESSAGE_SIZE = 1024;

        TcpListener _server;
        bool _running = false;
        protected List<TcpClient> clients = new List<TcpClient>();

        public BaseServer()
        {
            _server = new TcpListener(IPAddress.Parse("127.0.0.1"), 61882);
        }

        public void Start()
        {
            _server.Start();
            _running = true;
            Console.WriteLine("Started server");

            AcceptClients();
        }

        public void Stop()
        {
            _running = false;
            _server.Stop();
            Console.WriteLine("Stopped server");
        }

        public async void AcceptClients()
        {
            while (_running)
            {
                try
                {
                    var client = await _server.AcceptTcpClientAsync();
                    OnClientConnected(client);
                    Console.WriteLine("Client connected");
                    HandleClient(client);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error accepting client: {e.Message}");
                }
            }
        }

        private async void HandleClient(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[MAX_MESSAGE_SIZE];

                try
                {
                    while (client.Connected)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break; // Client disconnected

                        var data = new byte[bytesRead];
                        Array.Copy(buffer, data, bytesRead);
                        HandleMessage(client, data);
                    }
                    clients.Remove(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling client: {ex.Message}");
                    clients.Remove(client);
                }
            }
        }

        protected virtual void HandleMessage(TcpClient client, byte[] data)
        {
            Console.WriteLine($"Received message from {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port} Size: {data.Length}");
        }

        protected virtual void OnClientConnected(TcpClient client)
        {
            clients.Add(client);
        }
    }
}
