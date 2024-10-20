using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using YesingPokerCommon.Networking;

namespace YesingPokerServer
{
    public class NetworkTestServer : BaseServer
    {
        protected override void HandleMessage(TcpClient client, byte[] data)
        {
            base.HandleMessage(client, data);
            if (data.Length <= 0) return;

            foreach(var cli in clients)
            {
                cli.GetStream().Write(data, 0, data.Length);
            }
        }
    }
}
