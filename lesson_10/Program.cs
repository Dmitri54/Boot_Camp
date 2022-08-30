/* Сортировка подсчетом. 
Без модификаций она работает только с цифрами.

К примеру у нас есть массив 
[0, 2, 3, 2, 1, 5, 9, 1, 1]
от [0; 9] = цифры

Пока массив заполнен нолями
[0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
 0  1  2  3  4  5  6  7  8  9
Теперь пройду по массиву и посчитаю сколько цифр у меня есть и сколько они повторяются
[1, 3, 2, 1, 0, 1, 0, 0, 0, 1] 
 0  1  2  3  4  5  6  7  8  9
Теперь в отсортированный массив вставлю по порядку нужное количество элементов
[0, 1, 1, 1, 2, 2, 3, 5, 9] 
*/

/*
int[] array = {3, 2, 1, 5, 9};

CountingSort(array);
Console.WriteLine(string.Join(", ", array));

void CountingSort(int[] inputArray)
{
    int[] counters = new int[10];
    int ourNumber;
    for (int i = 0; i < inputArray.Length; i++) // массив повторений.
    {
        // counters[inputArray[i]]++; // Можно так записать, она равна нижним двум строкам.
        ourNumber = inputArray[i]; // ourNumber это то цисло, которое я сейчас обхожу в исходном массиве.
        counters[ourNumber]++;

    }
    int index = 0; 
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++) // Чтобы вставить элемент нужное количество раз, т.е. 1 три раза.
        {
            inputArray[index] = i; // Записываю в исходный массив по индексу повторения элементов.
            index++;
        }
    }
}

// первый вывод: 0, 1, 1, 1, 2, 2, 3, 5, 9 
// Второй вывод: 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5, 5, 6, 8, 9 - тут я добавил элем. в исходный массив.
// Третий вывод: 1, 2, 3, 5, 9 - уменьшил исходный массив.
*/
// -------------------------------------------------------------------------------------------------------------
/* Представля, что в исходном массиве минимальное число будет 0, а максимальный элемент неизвестен.
[0, 2, 4, 10, 20, 5, 6, 1, 2]
от [0, 20]
Я должен задать массив на 20 элементов +1.
[0, 0, 0, 0, 0, 0, 0,..0,..0]
 0  1  2  3  4  5  6  10  20 - индекс
Теперь пройду по массиву и посчитаю сколько цифр у меня есть и сколько они повторяются
[1, 1, 1, 0, 1, 1, 1, 1, 1]
 0  1  2  3  4  5  6 10 20
*/

/*int[] array = { 0, 2, 4, 10, 20, 5, 6, 1, 2 };

int[] sortedArray = CountingSortExtended(array);
Console.WriteLine(string.Join(", ", sortedArray));

int[] CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max(); // Найдет максимальный элемент. Встроенный метод.

    int[] sortedArray = new int[inputArray.Length];
    int[] counters = new int[max + 1];

    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i]]++;
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++) // Чтобы вставить элемент нужное количество раз, т.е. 1 три раза.
        {
            sortedArray[index] = i; // Записываю в исходный массив по индексу повторения элементов.
            index++;
        }
    }
    return sortedArray;
} */

// первый вывод: 0, 1, 2, 2, 4, 5, 6, 10, 20

/* [-10, -5, -9, 0, 2, 5, 1, 3, 1, 0, 1] 
Тут мне нужно задать смещение. Индексы counters будут смещенны на 10.
offset = 10; - т.к. минимальное значение -10.
counters[max + offset + 1]
*/

int[] array = {-1000, 1001, 100, 1001, 1002};

int[] sortedArray = CountingSortExtended(array);
Console.WriteLine(string.Join(", ", sortedArray));

int[] CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max(); // Найдет максимальный элемент. Встроенный метод.
    int min = inputArray.Min(); // Найдет минимальный элемент.

    int offset = -min; // от 0 отнему min. минус на минус даст +.
     
    int[] sortedArray = new int[inputArray.Length];
    int[] counters = new int[max + offset + 1];
    
    Console.WriteLine(max + offset + 1); // Напечатаю размер исходного массива, чтобы понять оптимизированно я делаю или нет.

    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i]+ offset]++; // Чтобы записать впервую ячейку отрицательный элемент, делаю смещение, для индекса. 
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++) // Чтобы вставить элемент нужное количество раз, т.е. 1 три раза.
        {
            sortedArray[index] = i - offset; // Записываю в исходный массив по индексу повторения элементов.
            index++;
        }
    }
    return sortedArray;
}

// первый вывод: -10, -9, -5, 0, 0, 1, 1, 1, 2, 3, 5
// второй вывод: 903
// 100, 1001, 1001, 1002
// третий вывод: 2003 - это очень большой размер памяти!!!
// -1000, 100, 1001, 1001, 1002

