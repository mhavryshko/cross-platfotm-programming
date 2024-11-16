using System;
using System.IO;
using System.Text;
using LAB1;
using LAB2;
using LAB3;

public class LabRunner
{
    public void RunLab1(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            
            string[] input = File.ReadAllLines(inputFile)[0].Split();
            int N = int.Parse(input[0]);
            int K = int.Parse(input[1]); 

           
            long result = LAB1.Program.CountWays(N, K);

            
            File.WriteAllText(outputFile, result.ToString());

            Console.WriteLine("File OUTPUT.TXT successfully created");
            Console.WriteLine("-----------LAB1-----------");
            Console.WriteLine($"The number of ways to place {K} rooks on a {N}x{N} chessboard: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void RunLab2(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;

            
            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            
            LAB2.Program.ProcessFile(inputFile, outputFile);

            Console.WriteLine("File OUTPUT.TXT successfully created");
            Console.WriteLine("-----------LAB2-----------");
            Console.WriteLine($"The minimum path has been calculated and saved to {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void RunLab3(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;

            
            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Input file not found.");
                return;
            }
            string[] inputLines = File.ReadAllLines(inputFile);
            Console.WriteLine("Input data:");
            foreach (var line in inputLines)
            {
                Console.WriteLine(line);
            }
            string result = LAB3.Program.ProcessBoard(inputLines);


            File.WriteAllText(outputFile, result);
            Console.WriteLine("-----------LAB3-----------");
            Console.WriteLine("Result successfully written to " + outputFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}