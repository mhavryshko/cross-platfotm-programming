using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Runtime.InteropServices;

[Command(Name = "LAB4", Description = "Console app for labs")]
[Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
public class Program
{
    static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private void OnExecute()
    {
        Console.WriteLine("Specify a command");
    }

    private void OnUnknownCommand(CommandLineApplication app)
    {
        Console.WriteLine("Unknown command. Use one of the following:");
        Console.WriteLine(" - version: Displays app version and author");
        Console.WriteLine(" - run: Run a specific lab");
        Console.WriteLine(" - set-path: Set input/output path");
    }
}

[Command(Name = "version", Description = "Displays app version and author")]
class VersionCommand
{
    private void OnExecute()
    {
        Console.WriteLine("Author: Mask Havryshko IPЗ-33");
        Console.WriteLine("Version: 1.0.0");
    }
}

[Command(Name = "run", Description = "Run a specific lab")]
class RunCommand
{
    [Argument(0, "lab", "Specify lab to run (lab1, lab2, lab3)")]
    public string Lab { get; set; } = string.Empty;

    [Option("--input", "Input file", CommandOptionType.SingleValue)]
    public string InputFile { get; set; } = string.Empty;

    [Option("--output", "Output file", CommandOptionType.SingleValue)]
    public string OutputFile { get; set; } = string.Empty;

    private void OnExecute()
    {
        var runner = new LabRunner();
        string labPath = GetLabDirectory(Lab);
        if (labPath == null)
        {
            Console.WriteLine($"Unknown lab '{Lab}'. Available labs: lab1, lab2, lab3.");
            return;
        }

        string inputFilePath = DetermineInputFile(InputFile);
        if (inputFilePath == null)
        {
            Console.WriteLine("Input file not found.");
            return;
        }

        string outputFilePath = GetOutputFilePath(labPath);
        if (Lab.ToLower() == "lab2")
        {
            runner.RunLab2(inputFilePath, outputFilePath);
        }
        else if (Lab.ToLower() == "lab1")
        {
            runner.RunLab1(inputFilePath, outputFilePath);
        }
        else if (Lab.ToLower() == "lab3")
        {
            runner.RunLab3(inputFilePath, outputFilePath);
        }
        else
        {
            Console.WriteLine("Unknown lab specified.");
        }

        Console.WriteLine($"Lab {Lab} processed. Output saved to {outputFilePath}");
    }

    private string DetermineInputFile(string inputFile)
    {
        if (!string.IsNullOrEmpty(inputFile) && File.Exists(inputFile))
        {
            return inputFile;
        }

        string labPath = Environment.GetEnvironmentVariable("LAB_PATH");
        if (!string.IsNullOrEmpty(labPath))
        {
            string envInputFilePath = Path.Combine(labPath, Path.GetFileName(inputFile));
            if (File.Exists(envInputFilePath))
            {
                return envInputFilePath;
            }
        }

        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string homeInputFilePath = Path.Combine(homeDirectory, Path.GetFileName(inputFile));
        if (File.Exists(homeInputFilePath))
        {
            return homeInputFilePath;
        }

        return null;
    }

    private string GetOutputFilePath(string labPath)
    {
        return Path.Combine(labPath, "OUTPUT.TXT");
    }

    private string GetLabDirectory(string labName)
    {
        string projectRoot = Directory.GetCurrentDirectory();

        switch (labName.ToLower())
        {
            case "lab1":
                return Path.Combine(projectRoot, "LAB1");
            case "lab2":
                return Path.Combine(projectRoot, "LAB2");
            case "lab3":
                return Path.Combine(projectRoot, "LAB3");
            default:
                return null;
        }
    }
}

[Command(Name = "set-path", Description = "Set input/output path")]
class SetPathCommand
{
    [Option("-p|--path", "Path to input/output files", CommandOptionType.SingleValue)]
    public string? Path { get; set; }

    private void OnExecute()
    {
        if (string.IsNullOrEmpty(Path))
        {
            Console.WriteLine("Path cannot be null or empty.");
            return;
        }

        Environment.SetEnvironmentVariable("LAB_PATH", Path);
        Console.WriteLine($"Path set to: {Path}");
    }
}