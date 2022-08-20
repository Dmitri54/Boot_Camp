/* Урок 9. Клиент-серверное взаимодействие. 
Тут будет описан класс Server */

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Это наш сервер");
            OurServer server = new OurServer();
        }
    }

}
