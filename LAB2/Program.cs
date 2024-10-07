using System;
using System.IO;
using System.Text;

namespace LAB2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Зчитування шляху до файлів
            string inputFilePath = args.Length > 0 ? args[0] : Path.Combine("LAB2", "INPUT.TXT");
            string outputFilePath = Path.Combine("LAB2", "OUTPUT.TXT");

            try
            {
                // Обробка файлу
                ProcessFile(inputFilePath, outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        // Метод для обробки файлу
        public static void ProcessFile(string inputFilePath, string outputFilePath)
        {
            // Зчитування вмісту файлу
            string[] lines = File.ReadAllLines(inputFilePath);
            int N = int.Parse(lines[0]); // Розмір матриці
            int[,] grid = new int[N, N];

            // Зчитування матриці
            for (int i = 0; i < N; i++)
            {
                string line = lines[i + 1];
                for (int j = 0; j < N; j++)
                {
                    grid[i, j] = line[j] - '0'; // Перетворення символу на цифру
                }
            }

            // Виведення зчитаної матриці
            Console.WriteLine($"Read matrix from {inputFilePath}:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            // Знаходження мінімального шляху
            char[,] result = FindMinimumPath(grid);
            PrintResult(result); // Виведення результату в консоль

            // Запис результату у файл
            WriteOutput(result, outputFilePath);
        }

        // Метод для знаходження мінімального шляху
        public static char[,] FindMinimumPath(int[,] grid)
        {
            int N = grid.GetLength(0);
            char[,] result = new char[N, N];
            int[,] dp = new int[N, N];
            (int, int)[,] path = new (int, int)[N, N];

            // Ініціалізація першої клітинки
            dp[0, 0] = grid[0, 0];

            // Заповнення першого рядка
            for (int j = 1; j < N; j++)
            {
                dp[0, j] = dp[0, j - 1] + grid[0, j];
                path[0, j] = (0, j - 1);
            }

            // Заповнення першого стовпця
            for (int i = 1; i < N; i++)
            {
                dp[i, 0] = dp[i - 1, 0] + grid[i, 0];
                path[i, 0] = (i - 1, 0);
            }

            // Заповнення решти матриці
            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < N; j++)
                {
                    if (dp[i - 1, j] < dp[i, j - 1])
                    {
                        dp[i, j] = dp[i - 1, j] + grid[i, j];
                        path[i, j] = (i - 1, j);
                    }
                    else
                    {
                        dp[i, j] = dp[i, j - 1] + grid[i, j];
                        path[i, j] = (i, j - 1);
                    }
                }
            }

            // Відновлення шляху
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result[i, j] = '.'; // Заповнення результату символом '.'
                }
            }

            int x = N - 1, y = N - 1;
            while (x != 0 || y != 0)
            {
                result[x, y] = '#'; // Позначення шляху символом '#'
                var previous = path[x, y];
                x = previous.Item1;
                y = previous.Item2;
            }
            result[0, 0] = '#'; // Включаємо початкову клітинку

            return result;
        }

        // Метод для виведення результату в консоль
        public static void PrintResult(char[,] result)
        {
            int N = result.GetLength(0);
            Console.WriteLine("Minimum path:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Метод для запису результату у файл
        public static void WriteOutput(char[,] result, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                int N = result.GetLength(0);
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        writer.Write(result[i, j]);
                    }
                    writer.WriteLine();
                }
            }
            Console.WriteLine($"The path has been written to {outputFilePath}.");
        }
    }
}
