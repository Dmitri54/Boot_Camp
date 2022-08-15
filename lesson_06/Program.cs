/* Урок 6. Быстрая сортировка.
к примеру у меня есть массив
          0  1   2  3  4  5  6 - индекс
1. arr = [1, 0, -6, 2, 5, 3, 2]
2. pivot == arr[6] - опорный элемент, можно взять крайний левой или крайний правый.
Моя задача поставить опорный элемент так, чтобы все элементы, которые меньше стояли левее, а все элементы, которые больше стояли справо.
[1, 0, -6, 2, 2, 5, 3] 
3. Далее мне нужно взять левый подмассив и правый подмассив и сделать с ними тоже самое.
т.е. Вызвать шаги 1, 2 для подмассива слева от pivot и справо от pivot. 
3.1. [1, 0, -6, 2] - левый подмассив.
3.2. pivot = 2
3.3. [1, 0, -6] - тут получилось, что уже все отсортированно.
4.1. [1, 0, -6] 
4.2. pivot = -6
4.3. [-6, 1, 0] 
5.1. [1, 0]
5.2. pivot = 0
5.3. [0, 1]
так же справым подмассивом
*/

int[] arr = { 0, -5, 2, 3, 5, 9, -1, 7 };
QuickSort(arr, 0, arr.Length - 1);
Console.Write($"Отсортированный массив {string.Join(", ", arr)}");

void QuickSort(int[] inputArray, int minIndex, int maxIndex) // Рекурсия
{
    if (minIndex >= maxIndex) return; // Условие выхода из рекурсии.
    int pivot = GetPivotIndex(inputArray, minIndex, maxIndex);
    QuickSort(inputArray, minIndex, pivot - 1); // Делю на левый подмассив.
    QuickSort(inputArray, pivot + 1, maxIndex); // Делю на правый подмассив.
    return;
}
int GetPivotIndex(int[] inputArray, int minIndex, int maxIndex) // Выдаст индекс опороного элемента
{
    int pivotIndex = minIndex - 1;
    for (int i = minIndex; i <= maxIndex; i++)
    {
        if (inputArray[i] < inputArray[maxIndex]) // Ищу все элементы, которые меньше опорного элемента.
        {
            pivotIndex++;
            // Swap(ref inputArray[pivot], ref inputArray[i]); // Swap -меняет местами. ref -это передача ссылки на массив, приэтом все изменяния, которые я сделаю передадуться и во вне.
            Swap(inputArray, i, pivotIndex);
        }
    }
    pivotIndex++;
    Swap(inputArray, pivotIndex, maxIndex);
    return pivotIndex;
}

// void Swap(ref int lefValue, ref int rightValue) // Передаю два элемента по ссылке. Первый вариант.
// {
//     int temp = lefValue;
//     lefValue = rightValue;
//     rightValue = temp;
// }

void Swap(int[] inputArray, int lefValue, int rightValue) // Передаю массив. Второй вариант.
{
    int temp = inputArray[lefValue];
    inputArray[lefValue] = inputArray[rightValue];
    inputArray[rightValue] = temp;
}

// Вывод
// Отсортированный массив -5, -1, 0, 2, 3, 5, 7, 9

