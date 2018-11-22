using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class ServerClass
    {
        private System.Windows.Forms.TextBox StatusTextBox;
        private List<TcpClient> connections;

        public ServerClass(System.Windows.Forms.TextBox StatusTextBox)
        {
            this.StatusTextBox = StatusTextBox;
            connections = new List<TcpClient>();
        }

        public void StartServer()
        {
            byte[] adr = { 127, 0, 0, 1 };
            IPAddress ipAdr = new IPAddress(adr);
            TcpListener newsock = new TcpListener(ipAdr, 12345);
            newsock.Start();
            while (true)
            {
                StatusTextBox.AppendText("Waiting for a client...");
                TcpClient client = newsock.AcceptTcpClient();
                connections.Add(client);
                StatusTextBox.AppendText("Client connected");
                Thread t0 = new Thread(() => ConnectionThread.RunT0(client, connections));
                t0.Start();
            }
        }
    }
}
