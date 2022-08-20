using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    class OurServer
    {
        TcpListener server;

        public OurServer()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            server.Start();

            LoopClients();
        }

        void LoopClients() // Что бы без конца ловить клиента
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }

        void HandleClient(TcpClient client) // Создам функцию, которая отдельно будет держать соединение с клиентом, чтобы не было разрывов.
        {
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            
            while (true)
            {
                string message = sReader.ReadLine(); // Считал от клиента строку
                Console.WriteLine($"Клиент написал - {message}");
            }
        }

    }
}