/* Урок 8. Параллельное программирование на примере умножения матриц.
*/
const int N = 1000; // размер матрицы
const int THREADS_NUMBER = 4; // количество потоков задаю сразу. Все константы называю с большой буквы.

int[,] serialMulRes = new int[N, N]; // результат умножения матриц в однопотоке
int[,] threadMulRes = new int[N, N]; // результат параллельного умножения матриц

int[,] firstMatrix = MatrixGenerator(N, N);
int[,] secondMatrix = MatrixGenerator(N, N);

SerialMatrixMul(firstMatrix, secondMatrix);
PrepareParallelMatrixMul(firstMatrix, secondMatrix);
Console.WriteLine(EqualityMatrix(serialMulRes, threadMulRes));


int[,] MatrixGenerator(int rows, int columns)
{
    Random _rand = new Random();
    int[,] res = new int[rows, columns];
    for (int i = 0; i < res.GetLength(0); i++)
    {
        for (int j = 0; j < res.GetLength(1); j++)
        {
            res[i, j] = _rand.Next(-100, 100);
        }
    }
    return res;
}

void SerialMatrixMul(int[,] a, int[,] b)
{
    if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы"); // Останавливает выплнение, если матрицы не равны.

    for (int i = 0; i < a.GetLength(0); i++)
    {
        for (int j = 0; j < b.GetLength(1); j++)
        {
            for (int k = 0; k < b.GetLength(0); k++)
            {
                serialMulRes[i, j] += a[i, k] * b[k, j];
            }
        }
    }
}

void PrepareParallelMatrixMul(int[,] a, int[,] b)
{
    if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы");
    int eachThreadCalc = N / THREADS_NUMBER; // Столько вычислений будет приходиться на каждый поток
    Thread[] arr = new Thread[2];
    var threadsList = new List<Thread>(); // создам место для хранения потоков. 
    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc; // задам диапазон. Позиция старта и позиция окончания. 
        int endPos = (i + 1) * eachThreadCalc;
        if (i == THREADS_NUMBER - 1) endPos = N;// если последний поток, делаю такую проверку.
        threadsList.Add(new Thread(() => ParallelMatrixMul(a, b, startPos, endPos))); // Создаю поток.
        threadsList[i].Start(); // Запуск потока.
    }
    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        threadsList[i].Join(); // Пройдет по каждому потоку и подождет, когда он закончит свою работу. Присоединит к главному потоку
    }
}

void ParallelMatrixMul(int[,] a, int[,] b, int startPos, int endPos)
{
    for (int i = startPos; i < endPos; i++)
    {
        for (int j = 0; j < b.GetLength(1); j++)
        {
            for (int k = 0; k < b.GetLength(0); k++)
            {
                threadMulRes[i, j] += a[i, k] * b[k, j];
            }
        }
    }
}

bool EqualityMatrix(int[,] fmatrix, int[,] smatrix) // Сравнивает две матрицы. Равны или не равны.
{
    bool res = true;

    for (int i = 0; i < fmatrix.GetLength(0); i++)
    {
        for (int j = 0; j < fmatrix.GetLength(1); j++)
        {
            res = res && (fmatrix[i, j] == smatrix[i, j]);
        }
    }
    return res;
}

