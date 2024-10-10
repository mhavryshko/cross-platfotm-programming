using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LAB3
{
    public class Program
    {
        static int N;
        static char[,] board;
        static (int, int) start, end;
        static int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 };
        static int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string inputFilePath = args.Length > 0 ? args[0] : Path.Combine("LAB3", "INPUT.TXT");
            string outputFilePath = Path.Combine("LAB3", "OUTPUT.TXT");

            string[] lines = File.ReadAllLines(inputFilePath);
            Console.WriteLine("Input data read from file:");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            string result = ProcessBoard(lines);
            Console.WriteLine("Processing complete. Writing result to output file.");

            // Write the result to the output file
            File.WriteAllText(outputFilePath, result);
            Console.WriteLine($"Result written to {outputFilePath}");
        }

        //public static string ProcessBoard(string[] lines)
        //{
        //    N = int.Parse(lines[0]);
        //    board = new char[N, N];

        //    for (int i = 0; i < N; i++)
        //    {
        //        string row = lines[i + 1];
        //        for (int j = 0; j < N; j++)
        //        {
        //            board[i, j] = row[j];
        //            if (row[j] == '@')
        //            {
        //                if (start == default)
        //                    start = (i, j);
        //                else
        //                    end = (i, j);
        //            }
        //        }
        //    }

        //    Console.WriteLine($"Board size: {N}x{N}");
        //    Console.WriteLine($"Start position: {start}");
        //    Console.WriteLine($"End position: {end}");

        //    if (BFS(start, end))
        //    {
        //        StringBuilder result = new StringBuilder();
        //        for (int i = 0; i < N; i++)
        //        {
        //            for (int j = 0; j < N; j++)
        //                result.Append(board[i, j]);
        //            result.AppendLine();
        //        }
        //        return result.ToString().Trim();
        //    }
        //    else
        //    {
        //        return "Impossible";
        //    }
        //}

        public static string ProcessBoard(string[] lines)
        {
            N = int.Parse(lines[0]);
            board = new char[N, N];
            start = (-1, -1);
            end = (-1, -1);

            for (int i = 0; i < N; i++)
            {
                string row = lines[i + 1];
                for (int j = 0; j < N; j++)
                {
                    board[i, j] = row[j];
                    if (row[j] == '@')
                    {
                        if (start == (-1, -1))
                            start = (i, j);
                        else
                            end = (i, j);
                    }
                }
            }

            if (BFS(start, end))
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                        result.Append(board[i, j]);
                    if (i != N - 1) // Добавляем новую строку только между строками, чтобы избежать лишней в конце
                        result.AppendLine();
                }
                return result.ToString().Trim();
            }
            else
            {
                return "Impossible";
            }
        }

        static bool BFS((int, int) start, (int, int) end)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            bool[,] visited = new bool[N, N];
            (int, int)[,] parent = new (int, int)[N, N];

            queue.Enqueue(start);
            visited[start.Item1, start.Item2] = true;
            parent[start.Item1, start.Item2] = (-1, -1);

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                Console.WriteLine($"Processing position: ({x}, {y})");

                if ((x, y) == end)
                {
                    MarkPath(parent, end);
                    Console.WriteLine("Path found.");
                    return true;
                }

                for (int i = 0; i < 8; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (IsValid(nx, ny) && !visited[nx, ny] && board[nx, ny] != '#')
                    {
                        queue.Enqueue((nx, ny));
                        visited[nx, ny] = true;
                        parent[nx, ny] = (x, y);
                        Console.WriteLine($"Added to queue: ({nx}, {ny})");
                    }
                }
            }

            Console.WriteLine("No path found.");
            return false;
        }

        static void MarkPath((int, int)[,] parent, (int, int) end)
        {
            var current = end;

            while (current != (-1, -1))
            {
                var (x, y) = current;
                if (board[x, y] != '@')
                    board[x, y] = '@';
                current = parent[x, y];
            }
        }

        static bool IsValid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < N && y < N;
        }
    }
}
