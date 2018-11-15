using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            byte[] buffer = new byte[1024];
            StringBuilder myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;


            //byte[] adr = { 127, 0, 0, 1 };
            //IPAddress ipAdr = new IPAddress(adr);
            //IPEndPoint ep = new IPEndPoint( ipAdr, 5010 );
            Console.WriteLine("Connecting to server...");
            TcpClient client = new TcpClient("127.0.0.1", 12345);
            Console.WriteLine("Connected");
            //client.Connect(ep);
            NetworkStream networkStream = client.GetStream();
            /*byte[] abyString = Encoding.ASCII.GetBytes("HEJ");
            networkStream.Write(abyString, 0, 3);*/

            Console.WriteLine("stream");
            do
            {
                Console.WriteLine("sio");
                numberOfBytesRead = networkStream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("fnsafs");
                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(buffer, 0, numberOfBytesRead));


            }
            while (networkStream.DataAvailable);

            Console.WriteLine("You received the following message : " + myCompleteMessage);
            Console.ReadKey();
        }
    }
}
