using YesingPokerCommon;
using YesingPokerCommon.Networking;

namespace YesingPokerClient.Windows
{
    public class NetworkTestWindow : BaseWindow
    {
        Texture background;
        BaseClient client = new BaseClient();

        public NetworkTestWindow(int _width, int _height) : base("Network test", _width, _height)
        {
            NewBackground();
            client.OnMessageRecieved += Client_OnMessageRecieved;
            client.Connect("127.0.0.1", 61882);
        }

        private void Client_OnMessageRecieved(byte[] data)
        {
            uint color = (uint)data[0] << 24 | (uint)data[1] << 16 | (uint)data[2] << 8 | data[3];
            NewBackground(color);
        }

        private void NewBackground(uint? color = null)
        {
            uint clr;
            if (color == null)
                clr = MousePos.X < WindowSize.X / 2 ? 0xff0000ff : 0x00ff00ff;
            else
                clr = (uint)color;
            background = Texture.CreateSolidColorTexture(Renderer, WindowSize.X, WindowSize.Y, clr);
        }

        public override void Render()
        {
            Renderer.DrawTexture(background, 0, 0);
        }

        public override void OnMouseUp()
        {
            byte[] messageBytes = new byte[4];
            Random.Shared.NextBytes(messageBytes);
            messageBytes[3] = byte.MaxValue;
            client.Send(messageBytes);
        }


    }
}
