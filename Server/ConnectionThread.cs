using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ConnectionThread
    {

        public static void RunT0(TcpClient client, List<TcpClient> connections)
        {
            string welcome = "Welcome to the DNPI1 test server";
            Broadcast(welcome, connections);

            byte[] buffer = new byte[1024];
            StringBuilder myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;

            while (true)
            {
                myCompleteMessage = new StringBuilder();
                NetworkStream networkStream = client.GetStream();
                numberOfBytesRead = networkStream.Read(buffer, 0, buffer.Length);
                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(buffer, 0, numberOfBytesRead));
                Broadcast(myCompleteMessage.ToString(), connections);
            }
        }

        public static void Broadcast(String msg, List<TcpClient> connections)
        {
            for(int i = 0; i < connections.Count; i++)
            {
                NetworkStream ns = connections[i].GetStream();
                byte[] data = Encoding.ASCII.GetBytes(msg);
                ns.Write(data, 0, data.Length);
                //ns.Close();
            }
        }
    }
}
