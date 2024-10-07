using System;
using System.IO;

namespace LAB1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = args.Length > 0 ? args[0] : Path.Combine("LAB1", "INPUT.TXT");
            string outputFilePath = Path.Combine("LAB1", "OUTPUT.TXT");

            // Перевірка існування вхідного файлу
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Файл не знайдено: " + inputFilePath);
                return;
            }

            // Читання вхідних даних
            string[] input = File.ReadAllLines(inputFilePath)[0].Split();
            int N = int.Parse(input[0]); // Розмір шахівниці
            int K = int.Parse(input[1]); // Кількість ладей

            long result = CountWays(N, K);

            
            File.WriteAllText(outputFilePath, result.ToString());

            Console.WriteLine("-----------LAB1-----------");
            Console.WriteLine("The number of ways to place rooks: " + result);
        }

        // Метод для обчислення кількості способів розстановки ладей
        public static long CountWays(int N, int K)
        {
            if (K > N) return 0; // Нельзя расставить больше ладей, чем размер доски

            long combinations = BinomialCoefficient(N, K); // Комбінації
            long permutations = Factorial(K); // Пермутації

            return combinations * permutations; // Повертаємо загальну кількість способів
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
