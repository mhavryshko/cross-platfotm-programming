using Microsoft.AspNetCore.Mvc;
using LAB5.Models;
using System.Text;

namespace LAB5.Controllers
{
    [Route("Lab")]
    public class LabController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LabController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [Route("Lab1")]
        public IActionResult Lab1()
        {
            var model = new LabViewModel
            {
                TaskNumber = "1",
                TaskVariant = "11",
                TaskDescription = "Потрібно знайти число способів розставити на шахівниці N×NK тур так, щоб вони не били один одного. Усі човни вважаються однаковими.",
                InputDescription = "У вхідному файлі INPUT.TXT записані натуральні числа N та K (N, K ≤ 8).",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть одне ціле число – відповідь завдання.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "8 8", Output = "40320" },
            }
            };
            return View(model);
        }
        [Route("Lab2")]
        public IActionResult Lab2()
        {
            var model = new LabViewModel
            {
                TaskNumber = "2",
                TaskVariant = "11",
                TaskDescription = "У таблиці з N рядків і N стовпців клітини заповнені цифрами від 0 до 9. " +
                "Потрібно знайти такий шлях із клітини (1, 1) у клітину (N, N), щоб сума цифр у клітинах, через які він пролягає, була мінімальною; " +
                "з будь-якої клітини ходити можна лише вниз чи праворуч.",
                InputDescription = "У першому рядку вхідного файлу INPUT.TXT знаходиться число N. У наступних N рядках містяться N цифр без пробілів. (2 ≤ N ≤ 250)",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть N рядків N символів. " +
                "Символ \"#\" (решітка) показує, що маршрут проходить через цю клітинку, а \".\" (Точка) - що не проходить. " +
                "Якщо шляхів із мінімальною сумою цифр кілька, можна вивести будь-який.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "3 \n" +
                                       "943 \n" +
                                       "216 \n" +
                                       "091", Output = "#..\n" +
                                                       "###\n" +
                                                       "..#"
                }
                }
            };
            return View(model);
        }
        [Route("Lab3")]
        public IActionResult Lab3()
        {
            var model = new LabViewModel
            {
                TaskNumber = "3",
                TaskVariant = "11",
                TaskDescription = "Дана шахова дошка, що складається з N×N клітин, кілька з них вирізано. " +
                "Провести ходом коня через невирізані клітини шлях мінімальної довжини однієї заданої клітини до іншої.",
                InputDescription = "У першому рядку вхідного файлу INPUT.TXT встановлено число N (2 ≤ N ≤ 50). У наступних N рядках міститься N символів. " +
                "Символом # позначена вирізана клітина, крапкою - невирізана клітина, @ - задані клітини (таких символів два), що відповідають початку та кінцю шляху коня.",
                OutputDescription = "Якщо шлях побудувати неможливо, у вихідний файл OUTPUT.TXT слід вивести \"Impossible\", інакше вивести таку саму карту, як і на вході, але помітити всі проміжні положення коня символом @.",
                TestCases = new List<TestCase>
            {
                new TestCase
                {
                    Input = "5\n.....\n.@@..\n.....\n.....\n.....",
                    Output = "...@.\n.@@..\n....@\n.....\n....."
                }
            }
            };
            return View(model);
        }
        public static string ConvertResultToString(char[,] result)
        {
            int N = result.GetLength(0);
            var sb = new StringBuilder();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sb.Append(result[i, j] + " ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
        [HttpPost]
        public async Task<IActionResult> ProcessLab(int labNumber, IFormFile inputFile)
        {
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest("Please upload a file");

            string[] lines;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                lines = fileContent.Split(Environment.NewLine);
            }

            string output = null;

            switch (labNumber)
            {
                case 1:
                    var inputCase1 = lines[0].Split();
                    int N1 = int.Parse(inputCase1[0]);
                    int K = int.Parse(inputCase1[1]);
                    output = LAB1.Program.CountWays(N1, K).ToString();
                    break;

                case 2:
                    int N2 = int.Parse(lines[0]);
                    int[,] grid = new int[N2, N2];

                    for (int i = 0; i < N2; i++)
                    {
                        string line = lines[i + 1];
                        for (int j = 0; j < N2; j++)
                        {
                            grid[i, j] = line[j] - '0';
                        }
                    }

                    char[,] result1 = LAB2.Program.FindMinimumPath(grid);

                    StringBuilder outputBuilder = new StringBuilder();
                    for (int i = 0; i < N2; i++)
                    {
                        for (int j = 0; j < N2; j++)
                        {
                            outputBuilder.Append(result1[i, j]);
                        }
                        outputBuilder.AppendLine();
                    }

                    output = outputBuilder.ToString().Trim();
                    break;
                case 3:
                    int size = int.Parse(lines[0]);
                    string[] boardInput = new string[size + 1];
                    Array.Copy(lines, 0, boardInput, 0, size + 1);

                    string boardResult = LAB3.Program.ProcessBoard(boardInput);

                    output = boardResult;
                    break;
                default:
                    return BadRequest("Invalid lab number");
            }

            var result = new { output = output };
            return Json(result);
        }

       

    }
}

