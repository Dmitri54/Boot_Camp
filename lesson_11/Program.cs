/* Урок 11. Продолжаем тему Сортировки подсчётом, но с параллельными вычислениями.
[0, 1, 2, 5, 1, 1, 3, 6] Threads = 2 - количество потоков
counters
[0, 0, 0, 0, 0, 0, 0]
 0  1  2  3  4  5  6 - index
Теперь пройду по массиву и посчитаю сколько элементов у меня есть и сколько раз они повторяются
[1, 3, 1, 1, 0, 1, 1]
 0  1  2  3  4  5  6
Теперь распараллелю вычисления, т.е. подслет различных частей массива между различными потоками.
([0, 1, 2, 5,) (1, 1, 3, 6]) - вот так

counters - будет общий, для всех потоков вычисления.
[0, 0, 0, 0, 0, 0, 0]
 0  1  2  3  4  5  6
Представлю, что все потоки, начинают работу одновременно.
parallel 
При записи в counters результата вычисления из первого и второго потока, может произойти конфликт, т.к. 
оба потока будут записывать в одну и туже ячейку counters результат (примет 1), в этом случае может произойти 
игнорирование первого потока и запись произойдет из второго, т.к. он был последним
[1,(2),1, 1, 0, 1, 1]
 0  1  2  3  4  5  6
(2) - это ошибка, должно быть 3.
Либо результат может быть не предсказуемым.
Мне нужно решить эту проблему!
Нужно прописать условие, которое позволит записывать по очереди.

В C# есть lock - это приметив синхронизации на платформе dotnet.
Если в ячейку памяти уже пишет один поток, второй поток ожидает, когда первый закончит и после этого второй 
поток запишет свои данные.
Данная "Гонка" может не проявляться, но это не значит, что этого не может произойти!
*/
const int THREADS_NAMBER = 4; // Четыре потока.
const int N = 100000;// Размер массива.
object locker = new object(); // Прописал синхронизацию между потоками. 

// int[] array = {-10, -5, -9, 0, 2, 5, 1, 3, 1, 0, 1};
// int[] sortedArray = CountingSortExtended(array);

Random rand = new Random(); // Создание массива случайных чисел в две строки.
int[] resSerial = new int[N].Select(r => rand.Next(0, 5)).ToArray(); // Создание массива, для последовательной 
// сортировки и записывается каждый элемент через рандом (0, 5) в переменную r и после вставляется в new int. 
// И так прохожу по всему массиву.
// Данная запись работает медленне если бы я написал заполнение массива через for each.

int[] resParallel = new int[N]; // Массив, для параллельной сортировки.

Array.Copy(resSerial, resParallel, N); // Копирую в resParallel. Разные копий, для сравнения.

// Console.WriteLine(string.Join(", ", resSerial));

CountingSortExtended(resSerial);
PrepareParallelCountingSort(resParallel);
Console.WriteLine(EqualityMatrix(resSerial, resParallel));

// Console.WriteLine(string.Join(", ", resSerial));
// Console.WriteLine(string.Join(", ", resParallel));

void PrepareParallelCountingSort(int[] inputArray) // Сделаю для подготовки параллельных вычислений, чтобы создать потоки.
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] counters = new int[max + offset + 1]; // Этот массив буду передовать, для различных потоков.

    // Поделю нагрузки между потоками [0, 50000) Thread 1, [50000, 100000) Thread 2.
    int eachThreadCale = N / THREADS_NAMBER;
    var threadsParall = new List<Thread>(); // Создал список, в который я могу динамически вставлять или удалять.

    for (int i = 0; i < THREADS_NAMBER; i++)
    {
        int startPos = i * eachThreadCale;
        int endPos = (i + 1) * eachThreadCale;
        if (i == THREADS_NAMBER - 1) endPos = N;
        threadsParall.Add(new Thread(() => CountingSortParallel(inputArray, counters, offset, startPos, endPos)));
        threadsParall[i].Start(); // Запустит принудительно поток.
    }
    foreach (var thread in threadsParall) // Подождет поток
    {
        thread.Join();
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

void CountingSortParallel(int[] inputArray, int[] counters, int offset, int startPos, int endPos)// Метод, вычисляет в параллельном режиме
{
    for (int i = startPos; i < endPos; i++)
    {
        lock (locker) // lock - ставит замок на ячейку
        {
            counters[inputArray[i] + offset]++;
        }
    }
}

void CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] counters = new int[max + offset + 1];


    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i] + offset]++;
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

bool EqualityMatrix(int[] fmatrix, int[] smatrix) // Для сравнения двух массивов первый при последовательной сортировки, второй при параллельной сортировки.
{
    bool res = true;
    for (int i = 0; i < N; i++)
    {
        res = res && (fmatrix[i] == smatrix[i]);
    }
    return res;
}

/* 
Первый вывод: 4 потока, 10 элементов
True
0, 1, 1, 1, 2, 2, 2, 4, 4, 4
0, 1, 1, 1, 2, 2, 2, 4, 4, 4 

Второй вывод: 100000 элементов
False

Третий вывод: 10 элем.
True

Четвертый вывод: 8 потока, 100000 элементов
False

Я вижу "Гонку" - т.к нет синхронизации между потоками!
Пятый вывод: 8 потока, 10000 элементов
True

Шестой вывод: 8 потока, 10000 элементов
False

Седьмой вывод: 4 потока, 10000 элементов
True

Прописал lock. 
*/