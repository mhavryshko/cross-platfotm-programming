using System;
using System.IO;

namespace LAB1
{
    public class Program
    {
        static void Main()
        {
            string inputPath = @"C:\Users\61sun\source\repos\cross-platfotm-programming\LAB1\INPUT.TXT";
            string outputPath = @"C:\Users\61sun\source\repos\cross-platfotm-programming\LAB1\OUTPUT.TXT";

            // Перевірка існування вхідного файлу
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Файл не знайдено: " + inputPath);
                return;
            }

            // Читання вхідних даних
            string[] input = File.ReadAllLines(inputPath)[0].Split();
            int N = int.Parse(input[0]); // Розмір шахівниці
            int K = int.Parse(input[1]); // Кількість ладей

            long result = CountWays(N, K); // Обчислення кількості способів

            // Запис результату у вихідний файл
            File.WriteAllText(outputPath, result.ToString());
        }

        // Метод для обчислення кількості способів розстановки ладей
        public static long CountWays(int N, int K)
        {
            if (K > N) return 0; // Нельзя расставить більше ладей, ніж розмір дошки

            long combinations = BinomialCoefficient(N, K); // Комбінації
            long permutations = Factorial(K); // Пермутації

            return combinations * combinations * permutations; // Повертаємо загальну кількість способів
        }

        // Метод для обчислення біноміального коефіцієнта
        public static long BinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;

            long result = 1;
            for (int i = 0; i < k; i++)
            {
                result *= (n - i);
                result /= (i + 1);
            }
            return result;
        }

        // Метод для обчислення факторіалу
        public static long Factorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
