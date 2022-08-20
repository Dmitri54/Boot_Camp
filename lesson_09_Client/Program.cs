/* Урок 9. Клиент-серверное взаимодействие. 
Тут будет описан класс Client */

namespace Client // namespace - пространство имен.
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Это наш клиент");
            OurClient ourClient = new OurClient();
        }
    }

}