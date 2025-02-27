﻿/* Урок 4. Алгоритм "Сортировка выбором" 
Есть массив [6, 15, 2, 9, -3]
Нужно отсортировать массив от min к max.
----------------------------------------------------------------------------------------------------
Решение.
Это сортировка методом выбора!
первый элемент приравняю к MIN = 6
сравниваю MIN со вторым числом
6 < 15
6 > 2
MIN = 2
2 < 9 
2 > -3
MIN = -3
Далее нужно изменить массив и положить MIN в первую яцейку.
[-3, 6, 15, 2, 9]
[6, 15, 2, 9] - тут я уже не трогаю первое число.
6 < 15
6 > 2
MIN = 2
2 < 9
[-3, 2, 6, 15, 9]
[6, 15, 9]
MIN = 6
6 < 15
6 < 9
[-3, 2, 6, 15, 9]
MIN = 15
15 > 9
[-3, 2, 6, 9, 15]
*/

Console.WriteLine("Введите количество элементов массива");
int n = Convert.ToInt32(Console.ReadLine());
// Заполнение массива
int[] array = new int[n];
for (int i = 0; i < n; i++)
{
    Console.Write("Введите число: ");
    array[i] = Convert.ToInt32(Console.ReadLine());
}
Console.WriteLine();
Console.WriteLine("Начальный массив: [" + string.Join(", ", array) + "]"); // Красивый вывод начального массива.

// Сортировка
for (int i = 0; i < n - 1; i++)
{
    int minIndex = i; // Завел локальный минимальный индекс.
    for (int j = i + 1; j < n; j++) // Завел ещё цикл для нахождения minIdex. Иду до n т.к. не факт, что последнее число окажется максимальным.
    {
        if (array[j] < array[minIndex])
            minIndex = j; // Когда выполняется в цикле (условия) выполняется только одно действие, фигурные скобки {} не ставятся.
    }
    int temp; // Вспомогательная переменная, для перемещения элемента.
    temp = array[minIndex];
    array[minIndex] = array[i];
    array[i] = temp;
}
Console.WriteLine("Конечный массив: [" + string.Join(", ", array) + "]"); // Красивый вывод конечного массива.

/* UTF-8 - это кодировка.
Console.WriteLine(10 > 7); // Сравнит и выдаст False или True.
При сравнении строк. 
Примеру:
привет - Эта строка будет больше, т.к. символ (п) стоит дальше по номеру чем символ (П).
Привет
------------------------------------------------------------------------------------------
Поэтому сравнение строк в программировании происходит через кодировку!
------------------------------------------------------------------------------------------
string[] array = new string[5];
for (int i = 0; i < 5; i++)
{
    array[i] = Console.ReadLine(); - Препод дал стотью по Console.ReadLine(); !!!
    https://docs.microsoft.com/ru-ru/dotnet/api/system.console.readline?view=net-6.0
}
Console.Write("[" + string.Join(", ", array) + "]");
for (int i = 0; i < 4; i++)
{
    int minIndex = i; 
    for (int j = i + 1; j < 5; j++) 
    {
        if (array[j].Length < array[minIndex].Length)
            minIndex = j;
    }
    string temp;
    temp = array[minIndex];
    array[minIndex] = array[i];
    array[i] = temp;
}
Console.WriteLine("Конечный массив: [" + string.Join(", ", array) + "]");
*/