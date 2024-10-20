using System.Net.Sockets;

namespace YesingPokerCommon.Networking
{
    public class BaseClient
    {
        protected TcpClient _client;

        public delegate void OnMessageRecievedHandler(byte[] data);
        public event OnMessageRecievedHandler OnMessageRecieved;

        public BaseClient()
        {
            _client = new TcpClient();
        }

        public void Connect(string ip, int port)
        {
            _client.Connect(ip, port);
            NetworkLoop();
        }

        public void Send(byte[] data)
        {
            _client.GetStream().Write(data, 0, data.Length);
        }

        private async void NetworkLoop()
        {
            try
            {
                Console.WriteLine("Connected to server.");

                NetworkStream stream = _client.GetStream();

                while (_client.Connected)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    OnMessageRecieved.Invoke(data);
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Client closed.");
            }
        }
    }
}
