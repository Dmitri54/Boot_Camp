using System.Net.Sockets;
using System.Text;


namespace Client
{
    class OurClient
    {
        private TcpClient client; // создал переменную при помощи, которой я могу работать с TSP. От клиента к серверу. Посути это и есть мой клиент.
        private StreamWriter sWriter; // Импровезированный поток, при помощи, которого я быду отправлять с клиента на сервер.
        private StreamReader sReader; // Создаю ещё один поток, чтобы получать сообщения от сервера.
        public OurClient()
        {
            client = new TcpClient("127.0.0.1", 5555);
            sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8); // Чтение
            sReader = new StreamReader(client.GetStream(), Encoding.UTF8); // Запись

            HandleComunicanion(); // Как только я создал клиента, нужно сразу это соедеинение держать.
        }
        
        void HandleComunicanion()
        {
            while (true) // Бесконечный цикл, для поддержания соединения с сервером.
            {
                Console.Write("> ");
                string? message = Console.ReadLine();
                sWriter.WriteLine(message);
                sWriter.Flush(); // Принудительно отправит сообщение сразу, после его создания.
                
                string answerServer = sReader.ReadLine();
                Console.WriteLine($"Сервер ответил -> {answerServer}");
            }
        }
    }
}