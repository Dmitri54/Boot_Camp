/* Урок 5. Пузырьковая сортировка (сравнение парных элементов).
[3, 1, 5, 0, 7, 9, 8] - начальный массив.

[3, 1] - я беру первый элемент и сравниваю их между собой.
1.[1, 3, 5, 0, 7, 9, 8]
2.[1, 3, 0, 5, 7, 9, 8]
3.[1, 0, 3, 5, 7, 9, 8]
4.[0, 1, 3, 5, 7, 9, 8]
5.[0, 1, 3, 5, 7, 8, 9]
Получается 5 шагов.
Можно подругому пройти.
*/
Console.Write("Введите кол-во элементов массива: ");
int n = Convert.ToInt32(Console.ReadLine());
int[] array = new int[n];
for (int i = 0; i < n; i++)
{
    Console.Write("Введите значение массива: ");
    array[i] = Convert.ToInt32(Console.ReadLine());
}
Console.WriteLine("Начальный массив: [" + string.Join(", ", array) + "]");
// Создал массив, заполнил, вывел на экран.
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n - 1; j++) // либо for (int j = 1; j < n; j++)
    {
        if (array[j] > array[j + 1]) // либо if (array[j - 1] > array[j]) 
        {
            int temp = array[j];
            array[j] = array[j + 1];
            array[j + 1] = temp;
        }
    }
    Console.WriteLine(i + "[" + string.Join(", ", array) + "]"); // Покажет каждый шаг прохода по массиву.
}



